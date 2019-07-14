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
    public class AmortizingPaymentSchemeTests
    {
        [Test]
        public void Should_calculate_one_period()
        {
            var calculator = new AmortizingPaymentScheme();
            var installments = calculator.GetPayments(1000, 0.12f, 1, 12);
            installments.Count().Should().Be(1);
            installments.First().AmountDue.Should().Be(1010);
            installments.First().Interests.Should().Be(10);
        }

        [Test]
        public void Should_calculate_two_installments()
        {
            var calculator = new AmortizingPaymentScheme();
            var installments = calculator.GetPayments(1000, 0.12f, 2, 12);
            installments.Count().Should().Be(2);
            installments.First().AmountDue.Should().Be(507.51m);
        }

        [TestCase(10000, 0.12f, 60)]
        [TestCase(2500000, 0.035f, 300)]
        public void Loan_should_be_fully_repaid(decimal initalLoan, float annualInterestRate, int periods)
        {
            var calculator = new AmortizingPaymentScheme();
            var payments = calculator.GetPayments(initalLoan, annualInterestRate, periods, 12);

            var remainingLoan = initalLoan;
            foreach (var payment in payments)
            {

                decimal interest = Math.Ceiling((decimal)((float)remainingLoan * 10000 * annualInterestRate / 12)) / 10000;
                remainingLoan = remainingLoan + interest - payment.AmountDue;
            }
            Math.Round(remainingLoan, 2).Should().Be(0);
        }
    }
}
