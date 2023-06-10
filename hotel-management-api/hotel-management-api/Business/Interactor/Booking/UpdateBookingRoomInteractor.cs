using hotel_management_api.APIs.Booking.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Booking
{
    public interface IUpdateBookingRoomInteractor 
    {
        public class Request
        {
            public BookingDto? Booking { set; get; }
            public string token { set; get; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public BookingDto? Booking { set; get; }
        }
        Task<IUpdateBookingRoomInteractor.Response> UpdateAsync(IUpdateBookingRoomInteractor.Request request);
    }
    public class UpdateBookingRoomInteractor : IUpdateBookingRoomInteractor
    {
        private readonly IBookingService bookingService;
        public UpdateBookingRoomInteractor(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }
        public async Task<IUpdateBookingRoomInteractor.Response> UpdateAsync(IUpdateBookingRoomInteractor.Request request) 
        {
            return await bookingService.UpdateAsync(request);
        }
    }
}
