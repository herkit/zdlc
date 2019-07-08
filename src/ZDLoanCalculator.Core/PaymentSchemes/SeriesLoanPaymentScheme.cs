using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZDLoanCalculator.Core.Models;

namespace ZDLoanCalculator.Core.PaymentSchemes
{
    public class SeriesLoanPaymentScheme : IPaymentScheme
    {
        public IEnumerable<Payment> GetPayments(decimal initialLoan, float interestRate, int totalPeriods, int periodsPerYear)
        {
            var interestRatePerPeriod = interestRate / periodsPerYear;
            decimal paymentPerPeriod = (decimal)(((double)initialLoan) / totalPeriods);

            var remainingLoan = initialLoan;
            for (var periodNo = 0; periodNo < totalPeriods; periodNo++) {
                decimal interestThisPeriod = (decimal)(((double)remainingLoan) * interestRatePerPeriod);
                yield return new Payment {
                    PeriodNumber = periodNo + 1,
                    Interests = Math.Round(interestThisPeriod, 3),
                    AmountDue = Math.Round(paymentPerPeriod + interestThisPeriod, 3)
                };
                remainingLoan = remainingLoan - paymentPerPeriod;
            }
        }
    }
}
