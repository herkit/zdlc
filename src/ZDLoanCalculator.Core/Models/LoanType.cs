using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZDLoanCalculator.Core.Models
{
    public class LoanType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Key { get; set; }

        public string Description { get; set; }

        public float InterestRate { get; set; }

    }
}
