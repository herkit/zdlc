using Microsoft.EntityFrameworkCore;
using ZDLoanCalculator.Core.Models;

namespace ZDLoanCalculator.Data.EntityFramework
{
    public class ZDLoanCalculatorContext : DbContext
    {
        public ZDLoanCalculatorContext(DbContextOptions<ZDLoanCalculatorContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoanType>().HasData(
                new LoanType { Key = "house", Description = "House loan", InterestRate = 0.035f },
                new LoanType { Key = "car", Description = "Car loan", InterestRate = 0.065f },
                new LoanType { Key = "consumer", Description = "Consumer loan", InterestRate = 0.105f }
            );
        }

        public DbSet<LoanType> LoanTypes { get; set; }
    }
}
