using System;
using System.Collections.Generic;
using System.Text;
using ZDLoanCalculator.Core.Models;

namespace ZDLoanCalculator.Core.PaymentSchemes
{
    public class AnnuityPaymentScheme : IPaymentScheme
    {
        public IEnumerable<Payment> GetPayments(decimal initalLoan, float interestRate, int totalPeriods, int periodsPerYear)
        {
            throw new NotImplementedException();
        }
    }
}
