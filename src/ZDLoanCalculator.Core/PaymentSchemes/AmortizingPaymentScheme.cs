using System;
using System.Collections.Generic;
using System.Text;
using ZDLoanCalculator.Core.Models;

namespace ZDLoanCalculator.Core.PaymentSchemes
{
    public class AmortizingPaymentScheme : IPaymentScheme
    {
        private const int DECIMALS = 2;
        private const int ACCURACY = 10000;

        public IEnumerable<Payment> GetPayments(decimal initalLoan, float interestRate, int totalPeriods, int periodsPerYear)
        {
            var interestPerPeriod = interestRate / periodsPerYear;
            double calculatedInstallment = Math.Ceiling((double)initalLoan * ACCURACY * interestPerPeriod * Math.Pow(1 + interestPerPeriod, totalPeriods) / (Math.Pow(1 + interestPerPeriod, totalPeriods) - 1));


            var remainingLoan = (double)initalLoan * ACCURACY;
            for (var periodNo = 0; periodNo < totalPeriods; periodNo++)
            {
                var interestThisPeriod = Math.Ceiling((double)remainingLoan * interestPerPeriod);
                remainingLoan = remainingLoan + interestThisPeriod;

                double actualPayment = calculatedInstallment;
                if (actualPayment > (double)remainingLoan) actualPayment = (double)remainingLoan;

                remainingLoan = (double)remainingLoan - actualPayment;

                yield return new Payment
                {
                    PeriodNumber = periodNo + 1,
                    AmountDue = (decimal)Math.Round(actualPayment / ACCURACY, DECIMALS),
                    Interests = (decimal)Math.Round(interestThisPeriod / ACCURACY, DECIMALS)
                };
            }
           
        }
    }
}
