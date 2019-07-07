namespace ZDLoanCalculator.Core.PaymentSchemes
{
    public interface IPaymentSchemeProvider
    {
        IPaymentScheme GetScheme(string schemeName);
    }
}