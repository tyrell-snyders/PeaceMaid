using Microsoft.AspNetCore.Mvc;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPayment payment) : ControllerBase
    {
        private readonly IPayment _payment = payment;

        [HttpPost]
        public async Task<IActionResult> Payment([FromBody]int Id, [FromBody]PaymentMethod paymentMethod)
        {
            var result = await _payment.PayAsync(Id, paymentMethod);

            return Ok(result);
        }
    }
}
