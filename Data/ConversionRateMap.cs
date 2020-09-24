using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data
{
    public class ConversionRateMap
    {
        public ConversionRateMap(EntityTypeBuilder<ConversionRate> entityBuilder)
        {
            entityBuilder.HasKey((cr => new { cr.From, cr.To }));
            entityBuilder.Property(cr => cr.To).IsRequired();
            entityBuilder.Property(cr => cr.From).IsRequired();
            entityBuilder.Property(cr => cr.Rate).IsRequired();
        }
    }
}
