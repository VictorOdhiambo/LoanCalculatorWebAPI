using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer_loanCalc.model
{
    public class ResponseBody
    {
        public string PaymentDate { get; set; }
        public double EMI { get; set; }
        public double Interest { get; set; }
        public double RemainingPrincipal { get; set; }
        public double PercentagePaid { get; set; }
        public double TotalRepaymentAmount { get; set; }
        public double AnnualRate { get; set; }
        public double ProcessingFee { get; set; }
    }
}
