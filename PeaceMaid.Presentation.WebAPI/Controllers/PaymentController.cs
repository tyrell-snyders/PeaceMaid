using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeaceMaid.Application.Interfaces;

namespace PeaceMaid.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController(IPayment payment) : ControllerBase
    {
        private readonly IPayment _payment = payment;

        [HttpPost("{id}")]
        public async Task<IActionResult> Payment([FromBody] string nonce, int id)
        {
            var result = await _payment.PayAsync(id, nonce);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentMethods()
        {
            return Ok(await _payment.GetPaymentMethodsAsync());
        }

        [HttpGet("generate-client-toke")]
        public IActionResult GenerateClientToken()
        {
            var clientToken = _payment.GenerateClientToken();
            return Ok(new { ClientToken = clientToken });
        }
    }
}
