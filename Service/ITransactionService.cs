using Data;
using System.Collections.Generic;

namespace Service
{
    public interface ITransactionService
    {
        void RemoveAll();

        void BulkInsert(IList<Transaction> entities);

        IEnumerable<Transaction> GetAllTransactions();

        void BackupTransactionsFromApi();

        TransactionsInformation GetTotalAmountBySku(string sku, string currency = "EUR");
    }
}
