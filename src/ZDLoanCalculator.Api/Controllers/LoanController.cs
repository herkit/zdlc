using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZDLoanCalculator.Api.Models;
using ZDLoanCalculator.Core.PaymentSchemes;

namespace ZDLoanCalculator.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILogger<LoanController> logger;
        private readonly IPaymentSchemeProvider paymentSchemeProvider;

        public LoanController(ILogger<LoanController> logger, IPaymentSchemeProvider paymentSchemeProvider)
        {
            this.logger = logger;
            this.paymentSchemeProvider = paymentSchemeProvider;
        }

        [Route("plan")]
        public ActionResult GetPaymentPlan([FromQuery] LoanRequest loanRequest)
        {
            try { 
                var scheme = paymentSchemeProvider.GetScheme(loanRequest.PaymentScheme);
                return Ok(scheme.GetPayments(loanRequest.LoanAmount, 0.035f, loanRequest.Periods, 12));
            }
            catch (ArgumentException argumentException)
            {
                if (argumentException.ParamName == "schemeName")
                    ModelState.AddModelError("schemeName", "Payment scheme must be a valid scheme");
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Something went wrong when getting payment plan");
                return StatusCode(500, "Ooops, somthing we did not expect has happened");
            }
        }
    }
}