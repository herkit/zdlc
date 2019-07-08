using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ZDLoanCalculator.Core.Data;

namespace ZDLoanCalculator.Data.EntityFramework
{
    public static class EntityFrameworkServiceConfiguration
    {
        public static IServiceCollection AddEFLoanDatabase(this IServiceCollection services)
        {
            services.AddDbContext<ZDLoanCalculatorContext>(options => options.UseSqlite("Data Source=Data/loans.db"));
            return services.AddScoped<ILoanTypeRepository, LoanTypeRepository>();
        }
    }
}
