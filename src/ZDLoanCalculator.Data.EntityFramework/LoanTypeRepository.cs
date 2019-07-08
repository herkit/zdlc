using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZDLoanCalculator.Core.Data;
using ZDLoanCalculator.Core.Models;

namespace ZDLoanCalculator.Data.EntityFramework
{
    public class LoanTypeRepository : ILoanTypeRepository
    {
        private ZDLoanCalculatorContext Context { get; set; }
        public LoanTypeRepository(ZDLoanCalculatorContext context)
        {
            Context = context;
        }
        public async Task<IEnumerable<LoanType>> GetAllAsync()
        {
            return await Context.LoanTypes.OrderBy(type => type.Description).ToListAsync();
        }

        public async Task<LoanType> GetAsync(string key)
        {
            return await Context.LoanTypes.FindAsync(key);
        }
    }
}
