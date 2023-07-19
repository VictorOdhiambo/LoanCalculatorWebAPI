using DomainLayer_loanCalc.model;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer_loanCalc;

namespace LoanCalculator_webAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanCalculatorController : Controller
    {
        private readonly IServiceLoan _serviceLoan;
        public LoanCalculatorController(IServiceLoan serviceLoan)
        {
            _serviceLoan = serviceLoan;
        }
   
        [HttpPost]
        [Route("/flat-rate")]
        public IActionResult GetInstallments(RequestBody body)
        {
            var res = _serviceLoan.GetInstallmentsFromFlatRateMethod(body.amount, body.period, body.frequency, body.startDate);
            return Ok(res);

        }


        [HttpPost]
        [Route("/reducing-balance")]
        public IActionResult GetInstallmentscByReducingBalance(RequestBody body)
        {
            var res = _serviceLoan.GetInstallmentsFromReducingBalance(body.amount, body.period, body.frequency, body.startDate);
            return Ok(res);
        }
       
    }

}
