using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data
{
    public class TransactionMap
    {
        public TransactionMap(EntityTypeBuilder<Transaction> entityBuilder)
        {
            entityBuilder.Property(tr => tr.Amount).IsRequired();
            entityBuilder.Property(tr => tr.Currency).IsRequired();
        }
    }
}
