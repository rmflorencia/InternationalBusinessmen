using Data;
using Repo;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;
using System.Globalization;
using Serilog;
using Newtonsoft.Json;

namespace Service
{
    public class TransactionService : ITransactionService
    {
        private IRepository<Transaction> transactionRepository;
        private IConversionRateService conversionRateService;
        private AdjacencyGraph<string, Edge<string>> graph;
        private Dictionary<Edge<string>, double> costs;
        private Dictionary<double, string> ratesPerCost;

        public TransactionService(IRepository<Transaction> transactionRepository, IConversionRateService conversionRateService)
        {
            this.transactionRepository = transactionRepository;
            this.conversionRateService = conversionRateService;
            ratesPerCost = new Dictionary<double, string>();
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            try
            {
                var dbTransactions = transactionRepository.GetAll().ToList();

                if (dbTransactions.Count() == 0)
                {
                    Log.Information("There are no available transactions");
                }
                else
                {
                    Log.Information("Log: Getting Transactions");
                    Log.Information("Log: Transactions were loaded successfully");
                }

                return dbTransactions;
            }
            catch (Exception dbEx)
            {
                Log.Error($"{dbEx}: Could not retrieve transactions from the database");
                throw;
            }            
        }       
    
        public void RemoveAll()
        {
            transactionRepository.RemoveAll();
        }

        public void BulkInsert(IList<Transaction> transactions)
        {
            transactionRepository.BulkInsert(transactions);

            try
            {
                transactionRepository.BulkInsert(transactions);                
            }
            catch (Exception dbEx)
            {
                Log.Error($"{dbEx}: Could not insert transaction records in the database");
            }
        }

        public void BackupTransactionsFromApi()
        {
            try
            {
                string responseString = string.Empty;
                using (var webClient = new WebClient())
                {
                    responseString = webClient.DownloadString("http://quiet-stone-2094.herokuapp.com/transactions.json");
                }
                RemoveAll();
                var transactions = JsonConvert.DeserializeObject<IList<Transaction>>(responseString);
                BulkInsert(transactions);
                Log.Information("Transactions were loaded successfully");                
           }
            catch (Exception ex)
            {              
                Log.Error($"{ex}: Service not available");
                throw;
            }
        }

        public TransactionsInformation GetTotalAmountBySku(string sku, string currency = "EUR")
        {
            decimal transactionAmount;
            decimal totalAmount = 0;

            IEnumerable<ConversionRate> conversionRates = conversionRateService.GetAllConversionRates().ToList();
            TransactionsInformation information = new TransactionsInformation { Transactions = new List<Transaction>() };

            if (conversionRates.Count() != 0)
            {       

                if (conversionRates.Any(r => r.From == currency))
                {
                    IEnumerable<Transaction> transactionsBySku = GetAllTransactions().Where(t => t.Sku == sku).ToList();
                    var numberOfTransactions = transactionsBySku.Count();
                    if (numberOfTransactions > 0)
                    {

                        SetUpRatesGraph(conversionRates);

                        foreach (var transaction in transactionsBySku)
                        {
                            if (!String.Equals(transaction.Currency, currency))
                            {
                                transactionAmount = AmountPerTransaction(transaction.Currency, currency, transaction.Amount, conversionRates);
                                transaction.Amount = Math.Round(transactionAmount, 2);
                            }
                            else
                            {
                                transactionAmount = transaction.Amount;
                            }

                            transaction.Currency = currency;
                            totalAmount += transactionAmount;
                            information.Transactions.Add(transaction);
                        }

                        information.TotalAmount = Math.Round(totalAmount, 2);
                        information.Message = $"Number of transactions: {numberOfTransactions}";

                        return information;
                    }
                    else
                    {
                        var message = "There are no transactions with the given sku";
                        information.Message = message;
                        Log.Information(message);
                        return information;
                    }
                }
                else
                {
                    var message = "The conversion rate is not available or is not valid";
                    information.Message = message;
                    Log.Information(message);
                    return information;
                }
            }
            else
            {
                var message = "There are no conversion rates available to perform the operation";
                information.Message = message;
                Log.Information(message);
                return information;
            }             
        }

        private void SetUpRatesGraph(IEnumerable<ConversionRate> conversionRates)
        {
            graph = new AdjacencyGraph<string, Edge<string>>();
            costs = new Dictionary<Edge<string>, double>();
            double i = 1;

            foreach (var item in conversionRates)
            {
                AddEdgeWithCosts(item.From, item.To, i);
                ratesPerCost.Add(i, item.Rate);
                i++;
            }           
        }

        private void AddEdgeWithCosts(string source, string target, double cost)
        {
            var edge = new Edge<string>(source, target);
            graph.AddVerticesAndEdge(edge);
            costs.Add(edge, cost);
        }

        private decimal AmountPerTransaction(string @from, string to, decimal amount, IEnumerable<ConversionRate> conversionRates)
        {
            var edgeCost = AlgorithmExtensions.GetIndexer(costs);
            var tryGetPath = graph.ShortestPathsDijkstra(edgeCost, @from);
            var totalAmount = amount;
            IEnumerable<Edge<string>> path;

            if (tryGetPath(to, out path))
            {
                foreach (var item in path)
                {
                    costs.TryGetValue(item, out double cost);

                    if (Decimal.TryParse(ratesPerCost[cost], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rate))
                    {
                        totalAmount *= rate;
                        totalAmount = Math.Round(totalAmount, 2);
                    }                    
                }
            }

            return totalAmount;
        }
    }
}
