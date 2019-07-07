using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ZDLoanCalculator.Api.Controllers;
using ZDLoanCalculator.Core;
using ZDLoanCalculator.Core.PaymentSchemes;

namespace ZDLoanCalculator.Test.Api
{
    [TestFixture]
    public class LoanControllerTests
    {
        [Test]
        public void Can_instantiate_controller()
        {
            var controller = new LoanController(null);
        }

        [Test]
        public void Should_retrieve_payment_plan_from_selected_scheme()
        {
            Payment_scheme_returns(
                new Payment { AmountDue = 1000 },
                new Payment { AmountDue = 900 }
            );

            var response = controller.GetPaymentPlan("house", "series", 1000000, 2);

            response.Count().Should().Be(2);
            response.First().AmountDue.Should().Be(1000);
            response.Last().AmountDue.Should().Be(900);
        }

        [SetUp]
        public void Setup()
        {
            this.mockPaymentSchemeProvider = new Mock<IPaymentSchemeProvider>();
            this.mockPaymentScheme = new Mock<IPaymentScheme>();
            this.mockPaymentSchemeProvider.Setup(p => p.GetScheme("series")).Returns(mockPaymentScheme.Object);

            controller = new LoanController(mockPaymentSchemeProvider.Object);


        }

        private void Payment_scheme_returns(params Payment[] payments)
        {
            mockPaymentScheme
                .Setup(ps => ps.GetPayments(It.IsAny<decimal>(), It.IsAny<float>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => payments.ToList());
        }

        private LoanController controller;
        private Mock<IPaymentSchemeProvider> mockPaymentSchemeProvider;
        private Mock<IPaymentScheme> mockPaymentScheme; 


    }
}
