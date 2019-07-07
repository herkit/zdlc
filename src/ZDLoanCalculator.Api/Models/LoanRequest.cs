using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZDLoanCalculator.Api.Models
{
    public class LoanRequest
    {
        [Required]
        public string PaymentScheme { get; set; }
        [Required]
        public string LoanType { get; set; }
        [Required]
        [Range(1, (double)decimal.MaxValue)]
        public decimal LoanAmount { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Periods { get; set; }
    }
}
