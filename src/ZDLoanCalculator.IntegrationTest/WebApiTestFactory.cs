using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using ZDLoanCalculator.Core.Data;
using ZDLoanCalculator.Core.PaymentSchemes;
using ZDLoanCalculator.Data.EntityFramework;

namespace ZDLoanCalculator.IntegrationTest
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<IStartup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder().UseStartup<IStartup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<ZDLoanCalculatorContext>(options =>
                {
                    options.UseInMemoryDatabase("InMmeoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ZDLoanCalculatorContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<WebApplicationFactory<IStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                        $"database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }

    [TestFixture]
    public class ApiTestFixture
    {



        private readonly CustomWebApplicationFactory<ZDLoanCalculator.Api.Startup> _factory;

        public ApiTestFixture()
        {
            _factory = new CustomWebApplicationFactory<ZDLoanCalculator.Api.Startup>();
        }

        [Test]
        public void TestLoanTypes()
        {
            var client = _factory.CreateClient();

            var response = client.GetAsync("/api/v1/loan/types");

            response.Result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
