
using Hotel.Application.DTOs.PaymentDtos;

namespace Hotel.Application.Interfaces
{
    public interface IStripePaymentService
    {
        Task<string> CreatePaymentAsync(CreatePaymentDto dto);
        Task HandleStripeWebhookAsync(string json, string stripeSignature);
    }
}
