using Hotel.Application.DTOs.PaymentDtos;
using Hotel.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;
using Hotel.Domain.Interfaces;
using CMS.Core.Entities;
using Stripe;
using Hotel.Domain.Enims;

namespace Hotel.Application.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _webhookSecret;
        private readonly IReservationService _reservationService;

        public StripePaymentService(IUnitOfWork unitOfWork, IConfiguration configuration, IReservationService reservationService)
        {
            _unitOfWork = unitOfWork;
            _webhookSecret = configuration["Stripe:WebhookSecret"];
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
            _reservationService = reservationService;
        }

        // 🟢 إنشاء دفع مرتبط بالحجز
        public async Task<string> CreatePaymentAsync(CreatePaymentDto dto)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(dto.ReservationId);
            if (reservation == null)
                throw new Exception("Reservation not found.");

            if (dto.Amount <= (reservation.TotalPrice * 0.2m))
                throw new Exception("20% or more of the reservation must be paid.");

            var domain = "https://localhost:44305/SWAGGER/index.html"; // غيرها للـ domain بتاعك

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(dto.Amount * 100), // Stripe بالـ cents
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"Reservation #{reservation.Id}"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = $"{domain}/success?session_id=CHECKOUT_SESSION_ID",//webhook
                //CancelUrl = $"{domain}/api/payments/cancel",
                Metadata = new Dictionary<string, string>
                {
                    { "ReservationId", reservation.Id.ToString() }
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            var payment = new Payment
            {
                ReservationId = reservation.Id,
                Amount = dto.Amount,
                Method = dto.Method,
                PaymentDate = DateTime.UtcNow
            };

            await _unitOfWork.PaymentRepository.CreateAsync(payment);

            return session.Url;
        }

         //🟢 التعامل مع Webhook من Stripe(للتأكيد على الدفع)
        public async Task HandleStripeWebhookAsync(string json, string stripeSignature)
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                stripeSignature,
                _webhookSecret
            );

            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                var reservationId = int.Parse(paymentIntent.Metadata["ReservationId"]);

                var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(r => r.Id == reservationId);
                if (reservation != null)
                {
                    reservation.Status = ReservationStatus.Confirmed;
                    _unitOfWork.ReservationRepository.UpdateAsync(reservation);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }
    }
}


