using GameGenesisApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace GameGenesisApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PayementController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] Payment payment)
        {
            StripeConfiguration.ApiKey = "sk_test_51NGhC2EJzwedTFPX4We4injqIDWjws5cO2vyBKcnbUGAzUnOceJ4sGCmAanCXtE0dwiZLeiMfZfzpJOTzQ70PGBY00t1uUyFQo";

            var options = new PaymentIntentCreateOptions
            {
                Amount = payment.amount,
                Currency = "eur",
                PaymentMethod = payment.paymentMethodId,
                Confirm = true,
            };
            var service = new PaymentIntentService();
            PaymentIntent intent;

            try
            {
                intent = await service.CreateAsync(options);
            }
            catch (StripeException e)
            {
                // Log exception or handle error
                return BadRequest(e.Message);
            }

            if (intent.Status == "succeeded")
            {
                // Payment succeeded, update your records accordingly
                return Ok();
            }
            else
            {
                // Payment failed
                return BadRequest("Payment failed");
            }
        }
    }
}
