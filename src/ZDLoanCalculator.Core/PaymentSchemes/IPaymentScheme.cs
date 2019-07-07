using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZDLoanCalculator.Core.PaymentSchemes
{
    public interface IPaymentScheme
    {
        IEnumerable<Payment> GetPayments(decimal initalLoan, float interestRate, int totalPeriods, int periodsPerYear);
    }
}
