using Data;
using Microsoft.EntityFrameworkCore;

namespace Repo
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ConversionRateMap(modelBuilder.Entity<ConversionRate>());
            new TransactionMap(modelBuilder.Entity<Transaction>());
        }
    }
}
