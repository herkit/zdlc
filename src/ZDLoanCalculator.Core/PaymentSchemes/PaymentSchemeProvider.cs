using System;
using System.Collections.Generic;
using System.Text;

namespace ZDLoanCalculator.Core.PaymentSchemes
{
    public class PaymentSchemeProvider : IPaymentSchemeProvider
    {
        public IEnumerable<KeyValuePair<string, string>> GetAll()
        {
            // TODO: Provide names based on list of IPaymentScheme implementations, use naming conventions to provide key?
            yield return new KeyValuePair<string, string>("series", "Series loan");
        }

        public IPaymentScheme GetScheme(string schemeName)
        {
            // TODO: Provide scheme from a list of IPaymentScheme implementations
            if (schemeName != "series")
                throw new ArgumentException("Only 'series' scheme is available at the moment", "schemeName");
            return new SeriesPaymentScheme();
        }
    }
}
