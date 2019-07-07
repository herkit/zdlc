using System;
using System.Collections.Generic;
using System.Text;

namespace ZDLoanCalculator.Core.PaymentSchemes
{
    public class PaymentSchemeProvider : IPaymentSchemeProvider
    {
        public IPaymentScheme GetScheme(string schemeName)
        {
            if (schemeName != "series")
                throw new ArgumentException("Only 'series' scheme is available at the moment", "schemeName");
            return new SeriesLoanPaymentScheme();
        }
    }
}
