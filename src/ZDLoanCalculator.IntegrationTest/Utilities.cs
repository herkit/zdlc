using System;
using System.Collections.Generic;
using System.Text;
using ZDLoanCalculator.Core.Models;
using ZDLoanCalculator.Data.EntityFramework;

namespace ZDLoanCalculator.IntegrationTest
{
    public class Utilities
    {
        public static void InitializeDbForTests(ZDLoanCalculatorContext db)
        {
            db.LoanTypes.AddRange(GetSeedingLoanTypes());
            db.SaveChanges();
        }

        private static LoanType[] GetSeedingLoanTypes()
        {
            return new[]
            {
                new LoanType { Key = "test", Description = "Test loan", InterestRate = 0.10f },
                new LoanType { Key = "cheap", Description = "Cheap test loan", InterestRate = 0.01f }
            };
        }
    }
}
