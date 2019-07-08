using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZDLoanCalculator.Api.Models;
using ZDLoanCalculator.Core.Data;
using ZDLoanCalculator.Core.PaymentSchemes;

namespace ZDLoanCalculator.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILogger<LoanController> logger;
        private readonly IPaymentSchemeProvider paymentSchemeProvider;
        private readonly ILoanTypeRepository loanTypeRepository;

        public LoanController(ILogger<LoanController> logger, IPaymentSchemeProvider paymentSchemeProvider, ILoanTypeRepository loanTypeRepository)
        {
            this.logger = logger;
            this.paymentSchemeProvider = paymentSchemeProvider;
            this.loanTypeRepository = loanTypeRepository;
        }

        [Route("plan")]
        public async Task<ActionResult> GetPaymentPlan([FromQuery] LoanRequest loanRequest)
        {
            try { 
                var scheme = paymentSchemeProvider.GetScheme(loanRequest.PaymentScheme);
                var loanType = await loanTypeRepository.GetAsync(loanRequest.LoanType);
                return Ok(scheme.GetPayments(loanRequest.LoanAmount, loanType.InterestRate, loanRequest.Periods, 12));
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

        [Route("types")]
        public async Task<ActionResult> GetLoanTypes()
        {
            return Ok(await loanTypeRepository.GetAllAsync());
        }
    }
}