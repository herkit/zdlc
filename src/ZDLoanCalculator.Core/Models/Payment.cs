﻿namespace ZDLoanCalculator.Core.Models
{
    public class Payment
    {
        public int PeriodNumber { get; set; }
        public decimal AmountDue { get; set; }
        public decimal Interests { get; set; }

    }
}