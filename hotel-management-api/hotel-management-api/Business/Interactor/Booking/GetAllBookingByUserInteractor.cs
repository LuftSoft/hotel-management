using hotel_management_api.APIs.Booking.DTOs;
using hotel_management_api.APIs.Hotel.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Booking
{
    public interface IGetAllBookingByUserInteractor
    {
        public class Request
        {
            public string token { set; get; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public List<BookingListDto>? BookingList { set; get; }
        }
        Task<IGetAllBookingByUserInteractor.Response> GetAsync(IGetAllBookingByUserInteractor.Request request);
    }
    public class GetAllBookingByUserInteractor: IGetAllBookingByUserInteractor
    {
        private readonly IBookingService bookingService;
        public GetAllBookingByUserInteractor(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }
        public async Task<IGetAllBookingByUserInteractor.Response> GetAsync(IGetAllBookingByUserInteractor.Request request)
        {
            return await bookingService.GetByUserIdAsync(request);
        }
    }
}
