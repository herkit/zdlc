using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZDLoanCalculator.Core.PaymentSchemes
{
    public static class PaymentSchemeServiceConfiguration
    {
        public static IServiceCollection AddPaymentSchemes(this IServiceCollection services)
        {
            services.AddSingleton<IPaymentSchemeProvider, PaymentSchemeProvider>();
            return services;
        }
    }
}
