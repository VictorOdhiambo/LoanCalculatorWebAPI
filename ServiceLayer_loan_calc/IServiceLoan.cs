using DomainLayer_loanCalc.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer_loanCalc
{
    public interface IServiceLoan
    {
        List<ResponseBody> GetInstallmentsFromFlatRateMethod(double amount, int period, double frequency, DateTime startDate);
        List<ResponseBody> GetInstallmentsFromReducingBalance(double amount, int period, double frequency, DateTime startDate);
    }
}
