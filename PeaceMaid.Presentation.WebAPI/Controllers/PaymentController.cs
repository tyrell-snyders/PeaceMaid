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

        [HttpPost("{id}")]
        public async Task<IActionResult> Payment([FromBody] PaymentMethod paymentMethod, int id)
        {
            var result = await _payment.PayAsync(id, paymentMethod);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentMethods()
        {
            return Ok(await _payment.GetPaymentMethodsAsync());
        }
    }
}
