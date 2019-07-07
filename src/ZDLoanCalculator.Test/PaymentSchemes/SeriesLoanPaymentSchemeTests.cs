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
    }
}
