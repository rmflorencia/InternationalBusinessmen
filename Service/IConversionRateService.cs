using Data;
using System.Collections.Generic;

namespace Service
{
    public interface IConversionRateService
    {       
        void RemoveAll();

        void AddRange(IEnumerable<ConversionRate> conversionRates);

        IEnumerable<ConversionRate> GetAllConversionRates();

        void BackupConversionRatesFromApi();
    }
}
