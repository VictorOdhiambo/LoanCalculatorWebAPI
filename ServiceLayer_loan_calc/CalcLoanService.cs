using DomainLayer_loanCalc.model;
using RepositoryLayer_loanCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer_loanCalc
{
    public class CalcLoanService : IServiceLoan
    {
        private ILoanRepo _loanRepo;
        public CalcLoanService(ILoanRepo loanRepo)
        {
            _loanRepo = loanRepo;
        }
        public List<ResponseBody> GetInstallmentsFromFlatRateMethod(double amount, int period, double frequecny, DateTime startDate)
        {
            return _loanRepo.CalculateInterestUsingFlatRate(amount, period, frequecny, startDate);
        }

        public List<ResponseBody> GetInstallmentsFromReducingBalance(double amount, int period, double frequecny, DateTime startDate)
        {
            return _loanRepo.CalculateInterestUsingReducingBalance(amount, period, frequecny, startDate);
        }
    }
}
