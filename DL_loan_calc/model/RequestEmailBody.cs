using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer_loanCalc.model
{
    public class RequestEmailBody
    {
        public string To { get; set; }  
        public string From { get; set; }  
        public string Subject { get; set; }  
        public string Content { get; set; }  
    }
}
