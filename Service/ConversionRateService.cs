using Data;
using Newtonsoft.Json;
using Repo;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Service
{
    public class ConversionRateService : IConversionRateService
    {
        private IRepository<ConversionRate> conversionRateRepository;


        public ConversionRateService(IRepository<ConversionRate> conversionRateRepository)
        {
            this.conversionRateRepository = conversionRateRepository;
        }

        public IEnumerable<ConversionRate> GetAllConversionRates()
        {
            try
            {
                var dbConversionRates = conversionRateRepository.GetAll().ToList();

                if (dbConversionRates.Count() == 0)
                {
                    Log.Information("There are no available conversion rates");
                }
                else
                {
                    Log.Information("Log: Getting Conversion Rates");
                    Log.Information("Log: Conversion Rates were loaded successfully");
                }

                return dbConversionRates;
            }
            catch (Exception dbEx)
            {
                Log.Error($"{dbEx}: Could not retrieve conversion rates from the database");
                throw;
            }
        }

        public void RemoveAll()
        {
            conversionRateRepository.RemoveAll();
        }

        public void AddRange(IEnumerable<ConversionRate> conversionRates)
        {
            conversionRateRepository.AddRange(conversionRates);
        }
               
        public void BackupConversionRatesFromApi()
        {
            try
            {
                string responseString = string.Empty;
                using (var webClient = new WebClient())
                {
                    responseString = webClient.DownloadString("http://quiet-stone-2094.herokuapp.com/rates.json");
                }

                RemoveAll();
                var conversionRates = JsonConvert.DeserializeObject<IEnumerable<ConversionRate>>(responseString);
                AddRange(conversionRates);
            }
            catch (Exception ex)
            {
                Log.Information($"{ex}: The service is not available");
                throw;
            }
        }

    }
}
