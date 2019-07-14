using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using ZDLoanCalculator.Core.PaymentSchemes;
using System.Linq;

namespace ZDLoanCalculator.Test.PaymentSchemes
{
    [TestFixture]
    public class AnnuityPaymentSchemeTests
    {
        [Test]
        public void Should_calculate_one_period()
        {
            var calculator = new AnnuityPaymentScheme();
            var installments = calculator.GetPayments(1000, 0.12f, 1, 12);
            installments.Count().Should().Be(1);
            installments.First().AmountDue.Should().Be(1010);
            installments.First().Interests.Should().Be(10);
        }

        [Test]
        public void Should_calculate_two_installments()
        {
            var calculator = new AnnuityPaymentScheme();
            var installments = calculator.GetPayments(1000, 0.12f, 2, 12);
            installments.Count().Should().Be(2);
            installments.First().AmountDue.Should().Be(507.51m);
        }

        [TestCase(10000, 0.12f, 60)]
        [TestCase(2500000, 0.035f, 300)]
        public void Loan_should_be_fully_repaid(decimal initalLoan, float annualInterestRate, int periods)
        {
            var calculator = new AnnuityPaymentScheme();
            var payments = calculator.GetPayments(initalLoan, annualInterestRate, periods, 12);

            var remainingLoan = initalLoan;
            foreach (var payment in payments)
            {
                decimal interests = Math.Round((decimal)((double)remainingLoan * annualInterestRate / 12), 2);
                remainingLoan = remainingLoan + interests - payment.AmountDue;
            }
            Math.Round(remainingLoan, 2).Should().Be(0);
        }
    }
}
