using System.Collections.Generic;
using ZDLoanCalculator.Core.Models;

namespace ZDLoanCalculator.Core.PaymentSchemes
{
    public interface IPaymentScheme
    {
        IEnumerable<Payment> GetPayments(decimal initalLoan, float interestRate, int totalPeriods, int periodsPerYear);
    }
}
