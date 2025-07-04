using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Core;

namespace PayrollSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollCalculatorController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public ActionResult<decimal> Calculate([FromBody] Employee employee)
        {
           var calculator = new PayrollCalculator();
           var result = calculator.Calculate(employee);
           return Ok(result);
        }
    }
}
