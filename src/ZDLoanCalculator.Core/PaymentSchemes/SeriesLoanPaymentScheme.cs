using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZDLoanCalculator.Core.PaymentSchemes
{
    public class SeriesLoanPaymentScheme : IPaymentScheme
    {
        public IEnumerable<Payment> GetPayments(decimal initialLoan, float interestRate, int totalPeriods, int periodsPerYear)
        {
            var interestRatePerPeriod = interestRate / periodsPerYear;
            decimal interestThisPeriod = (decimal)(((double)initialLoan * 1000) * interestRatePerPeriod) / 1000;
            yield return new Payment { AmountDue = Math.Round(initialLoan + interestThisPeriod, 3) };
        }
    }
}
