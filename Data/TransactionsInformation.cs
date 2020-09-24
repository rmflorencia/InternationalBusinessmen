using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class TransactionsInformation
    {
        public List<Transaction> Transactions { get; set; }

        public decimal TotalAmount { get; set; }

        public string Message { get; set; }
    }
}
