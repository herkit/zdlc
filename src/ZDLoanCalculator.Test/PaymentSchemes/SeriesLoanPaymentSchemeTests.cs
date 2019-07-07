using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using ZDLoanCalculator.Core.PaymentSchemes;

namespace ZDLoanCalculator.Test.PaymentSchemes
{
    [TestFixture]
    public class SeriesLoanPaymentSchemeTests
    {
        [Test]
        public void Should_be_able_to_calculate_one_period()
        {
            var scheme = new SeriesLoanPaymentScheme();
            var payments = scheme.GetPayments(1000, 0.12f, 1, 12);
            payments.Count().Should().Be(1);
            payments.First().AmountDue.Should().Be(1010);
        }

        [Test]
        public void Should_be_able_to_calculate_multiple_periods()
        {
            var scheme = new SeriesLoanPaymentScheme();
            var payments = scheme.GetPayments(1000, 0.12f, 4, 12).GetEnumerator();
            payments.MoveNext();
            payments.Current.PeriodNumber.Should().Be(1);
            payments.Current.Interests.Should().Be(10m);
            payments.Current.AmountDue.Should().Be(260);
            payments.MoveNext();
            payments.Current.PeriodNumber.Should().Be(2);
            payments.Current.Interests.Should().Be(7.5m);
            payments.Current.AmountDue.Should().Be(257.5m);
            payments.MoveNext();
            payments.Current.PeriodNumber.Should().Be(3);
            payments.Current.Interests.Should().Be(5m);
            payments.Current.AmountDue.Should().Be(255m);
            payments.MoveNext();
            payments.Current.PeriodNumber.Should().Be(4);
            payments.Current.Interests.Should().Be(2.5m);
            payments.Current.AmountDue.Should().Be(252.5m);
        }
    }
}
