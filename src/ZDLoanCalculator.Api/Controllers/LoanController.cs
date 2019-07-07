using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZDLoanCalculator.Core;
using ZDLoanCalculator.Core.PaymentSchemes;

namespace ZDLoanCalculator.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly IPaymentSchemeProvider paymentSchemeProvider;

        public LoanController(IPaymentSchemeProvider paymentSchemeProvider)
        {
            this.paymentSchemeProvider = paymentSchemeProvider;
        }

        [Route("{loanType}/{schemeName}")]
        public IEnumerable<Payment> GetPaymentPlan(string loanType, string schemeName, decimal loanAmount, int periods)
        {
            var scheme = paymentSchemeProvider.GetScheme(schemeName);
            return scheme.GetPayments(loanAmount, 0.035f, periods, 12);
        }
    }
}