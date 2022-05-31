using Mapster;
using Movie.Data;
using Movie.Web.Services.Abstractions;
using Movie.Web.Services.Exceptions;
using Movie.Web.Services.Models;
using System;
using System.Threading.Tasks;

namespace Movie.Web.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        public readonly IPaymentRepository _paymentRepository;
        public readonly IBookingService _bookingService;

        public PaymentService(IPaymentRepository paymentRepository, IBookingService bookingService)
        {
            _paymentRepository = paymentRepository;
            _bookingService = bookingService;
        }

        public async Task CreatePaymentAsync(Guid bookingId, Guid userId)
        {
            //Check if bookig id exist
            if (!await _bookingService.IsExistAsync(bookingId))
                throw new NotExistException($"Booking Id:{bookingId} doesn't exist");

            //Create Payment Model
            Payment paymentModel = new Payment()
            {
                BookingId = bookingId,
                UserId = userId,
                PayemntDT = DateTime.UtcNow,
            };

            await _paymentRepository.CreatePaymentAsync(paymentModel.Adapt<Domain.POCO.Payment>());
        }
    }
}
