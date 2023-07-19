using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer_loanCalc.model
{
    /**
     * ENUM FOR DIFFERENT FREQUENCY OPTIONS FOR LOAN REPAYMENT
     */
    public class Frequency
    {
        public static readonly string ANNUALLY = "ANNUALLY";
        public static readonly string QUARTERLY = "QUARTERLY";
        public static readonly string SEMI_ANNUALLY = "SEMI_ANNUALLY";
        public static readonly string MONTHLY = "MONTHLY";
    }
}
