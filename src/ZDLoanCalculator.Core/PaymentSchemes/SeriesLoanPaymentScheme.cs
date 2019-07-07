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
            decimal paymentPerPeriod = (decimal)(((double)initialLoan * 1000) / totalPeriods) / 1000;

            var remainingLoan = initialLoan;
            for (var periodNo = 0; periodNo < totalPeriods; periodNo++) {
                decimal interestThisPeriod = (decimal)(((double)remainingLoan * 1000) * interestRatePerPeriod) / 1000;
                yield return new Payment { AmountDue = Math.Round(paymentPerPeriod + interestThisPeriod, 3) };
                remainingLoan = remainingLoan - paymentPerPeriod;
            }
        }
    }
}
