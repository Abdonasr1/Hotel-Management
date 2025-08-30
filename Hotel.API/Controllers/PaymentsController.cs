using Hotel.Application.DTOs.PaymentDtos;
using Hotel.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IStripePaymentService _paymentService;

        public PaymentsController(IStripePaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // 🟢 إنشاء دفع
        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment(CreatePaymentDto dto)
        {
            var checkoutUrl = await _paymentService.CreatePaymentAsync(dto);
            return Ok(new { url = checkoutUrl });
        }


        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeSignature = Request.Headers["Stripe-Signature"];

            try
            {
                await _paymentService.HandleStripeWebhookAsync(json, stripeSignature);
                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(e.Message);
            }
            //return Ok();
        }
    }

}



