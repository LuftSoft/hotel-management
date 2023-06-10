using hotel_management_api.APIs.Booking.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Booking
{
    public interface ICancelBookingRoomInteractor
    {
        public class Request
        {
            public string token { set; get; }
            public int BookingId { set; get; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
        Task<ICancelBookingRoomInteractor.Response> CancelAsync(ICancelBookingRoomInteractor.Request request);
    }
    public class CancelBookingRoomInteractor : ICancelBookingRoomInteractor
    {
        private readonly IBookingService bookingService;
        public CancelBookingRoomInteractor(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }
        public async Task<ICancelBookingRoomInteractor.Response> CancelAsync(ICancelBookingRoomInteractor.Request request)
        {
            return await bookingService.CancelAsync(request);
        }
    }
}
