using hotel_management_api.APIs.Booking.DTOs;
using hotel_management_api.APIs.Hotel.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Booking
{
    public interface ICreateBookingRoomInteractor
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
        Task<ICreateBookingRoomInteractor.Response> CreateAsync(ICreateBookingRoomInteractor.Request request);
    }
    public class CreateBookingRoomInteractor: ICreateBookingRoomInteractor
    {
        private readonly IBookingService bookingService;
        public CreateBookingRoomInteractor(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        public async Task<ICreateBookingRoomInteractor.Response> CreateAsync(ICreateBookingRoomInteractor.Request request)
        {
            return await bookingService.CreateAsync(request);
        }
    }
}
