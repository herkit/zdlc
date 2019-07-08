using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ZDLoanCalculator.Api.Controllers;
using ZDLoanCalculator.Api.Models;
using ZDLoanCalculator.Core.PaymentSchemes;
using ZDLoanCalculator.Core.Models;
using ZDLoanCalculator.Core.Data;
using System.Threading.Tasks;

namespace ZDLoanCalculator.Test.Api
{
    [TestFixture]
    public class LoanControllerTests
    {
        [Test]
        public void Can_instantiate_controller()
        {
            var controller = new LoanController(null, null, null);
        }

        [Test]
        public async Task Should_retrieve_payment_plan_from_selected_scheme()
        {
            Payment_scheme_returns(
                new Payment { AmountDue = 1000 },
                new Payment { AmountDue = 900 }
            );
            mockLoanTypeRepository
                .Setup(r => r.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new LoanType()));

            var response = await controller
                .GetPaymentPlan(
                    new LoanRequest {
                        LoanType = "house",
                        PaymentScheme = "series",
                        LoanAmount = 1000000,
                        Periods = 2
                    });

            response.Should().BeOfType<OkObjectResult>();
            var okResult = response as OkObjectResult;
            var payments = okResult.Value as IEnumerable<Payment>;
            payments.Count().Should().Be(2);
            payments.First().AmountDue.Should().Be(1000);
            payments.Last().AmountDue.Should().Be(900);
        }

        [Test]
        public async Task Should_return_bad_request_on_non_existing_scheme()
        {
            mockPaymentSchemeProvider.Setup(p => p.GetScheme("nonexisting")).Throws(new ArgumentException("", "schemeName"));

            var response = await controller
                .GetPaymentPlan(
                    new LoanRequest
                    {
                        LoanType = "house",
                        PaymentScheme = "nonexisting",
                        LoanAmount = 1000000,
                        Periods = 2
                    });

            response.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = response as BadRequestObjectResult;
            badRequestResult.Value.Should().BeOfType<SerializableError>();
        }

        [Test]
        public async Task Should_return_bad_request_on_non_existing_loan_type()
        {
            mockLoanTypeRepository
                .Setup(p => p.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((LoanType)null));

            var response = await controller
                .GetPaymentPlan(
                    new LoanRequest
                    {
                        LoanType = "nonexisting",
                        PaymentScheme = "series",
                        LoanAmount = 1000000,
                        Periods = 2
                    });

            response.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = response as BadRequestObjectResult;
            badRequestResult.Value.Should().BeOfType<SerializableError>();
        }

        [TestCase(0.035f)]
        [TestCase(0.105f)]
        public void Should_use_interest_rate_from_loan_type(float interestRate)
        {
            Returned_loan_type_has(interestRate);

            var response = controller
                .GetPaymentPlan(
                    new LoanRequest
                    {
                        LoanType = "house",
                        PaymentScheme = "series",
                        LoanAmount = 1000000,
                        Periods = 2
                    });

            mockPaymentScheme.Verify(s => s.GetPayments(1000000, interestRate, 2, 12), Times.Once());
        }

        [Test]
        public async Task Should_return_list_of_loan_types()
        {
            mockLoanTypeRepository
                .Setup(r => r.GetAllAsync())
                .Returns(Task.FromResult(new List<LoanType>
                    {
                        new LoanType { Key = "house", Description = "House loan", InterestRate = 0.035f },
                        new LoanType { Key = "car", Description = "Car loan", InterestRate = 0.065f }
                    }.AsEnumerable())
                );

            var response = await controller.GetLoanTypes();

            response.Should().BeOfType<OkObjectResult>();

            var okResult = response as OkObjectResult;
            var loanTypes = okResult.Value as IEnumerable<LoanType>;

            loanTypes.Count().Should().Be(2);
            loanTypes.First().Key.Should().Be("house");
            loanTypes.Last().Key.Should().Be("car");
        }

        [SetUp]
        public void Setup()
        {
            this.mockPaymentSchemeProvider = new Mock<IPaymentSchemeProvider>();
            this.mockPaymentScheme = new Mock<IPaymentScheme>();
            this.mockPaymentSchemeProvider.Setup(p => p.GetScheme("series")).Returns(mockPaymentScheme.Object);
            this.mockLoanTypeRepository = new Mock<ILoanTypeRepository>();

            controller = new LoanController(null, mockPaymentSchemeProvider.Object, mockLoanTypeRepository.Object);
        }

        private void Payment_scheme_returns(params Payment[] payments)
        {
            mockPaymentScheme
                .Setup(ps => ps.GetPayments(It.IsAny<decimal>(), It.IsAny<float>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => payments.ToList());
        }

        private void Returned_loan_type_has(float interestRate)
        {
            mockLoanTypeRepository
                .Setup(r => r.GetAsync(It.IsAny<string>()))
                .Returns<string>((key) => Task.FromResult(
                    new LoanType { InterestRate = interestRate, Key = key }
                ));
        }

        private LoanController controller;
        private Mock<IPaymentSchemeProvider> mockPaymentSchemeProvider;
        private Mock<IPaymentScheme> mockPaymentScheme;
        private Mock<ILoanTypeRepository> mockLoanTypeRepository;

    }
}
