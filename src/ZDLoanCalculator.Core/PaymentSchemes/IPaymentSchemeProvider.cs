using System.Collections.Generic;

namespace ZDLoanCalculator.Core.PaymentSchemes
{
    public interface IPaymentSchemeProvider
    {
        IPaymentScheme GetScheme(string schemeName);

        IEnumerable<KeyValuePair<string, string>> GetAll();
    }
}