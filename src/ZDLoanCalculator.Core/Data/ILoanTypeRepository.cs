using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZDLoanCalculator.Core.Models;

namespace ZDLoanCalculator.Core.Data
{
    public interface ILoanTypeRepository
    {
        Task<IEnumerable<LoanType>> GetAllAsync();
        Task<LoanType> GetAsync(string key);
    }
}
