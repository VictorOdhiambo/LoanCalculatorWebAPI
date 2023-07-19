using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer_loanCalc.model
{
    public class RequestBody
    {
        public double amount { get; set; }
        public int period { get; set; }
        public int frequency { get; set; }
        public DateTime startDate { get; set; }
    }
}
