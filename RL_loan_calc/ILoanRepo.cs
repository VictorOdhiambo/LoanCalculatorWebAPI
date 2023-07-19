using DomainLayer_loanCalc.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer_loanCalc
{
    public interface ILoanRepo
    {
        List<ResponseBody> CalculateInterestUsingFlatRate(double amount, int period, double rate, DateTime startDate);
        List<ResponseBody> CalculateInterestUsingReducingBalance(double amount, int period, double rate, DateTime startDate);
    }
}
