using Data;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Collections.Generic;

namespace Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService transactionService;
        private readonly IConversionRateService conversionRateService;

        public TransactionsController(ITransactionService transactionService, IConversionRateService conversionRateService)
        {
            this.transactionService = transactionService;
            this.conversionRateService = conversionRateService;
        }

        [HttpGet]
        public IEnumerable<ConversionRate> GetAllConversionRates()
        {
            return conversionRateService.GetAllConversionRates();            
        }

        [HttpGet]
        public IEnumerable<Transaction> GetAllTransactions()
        {
            return transactionService.GetAllTransactions();
        }

        [HttpGet]
        public TransactionsInformation GetTotalAmountBySku(string sku, string currency = "EUR")
        {
            return transactionService.GetTotalAmountBySku(sku, currency);
        }

        [HttpGet]
        public void BackupTransactionsFromApi()
        {
           transactionService.BackupTransactionsFromApi();
        }

        [HttpGet]
        public void BackupConversionRatesFromApi()
        {
            conversionRateService.BackupConversionRatesFromApi();
        }

    }
}
