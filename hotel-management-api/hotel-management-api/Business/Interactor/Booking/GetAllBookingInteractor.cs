using hotel_management_api.APIs.Booking.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Booking
{
    public interface IGetAllBookingInteractor
    {
        public class Request
        {
            public int pageSize { set; get; }
            public int pageIndex { set; get; }
        }
            public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public List<BookingListDto>? BookingList { set; get; }
        }
        Task<IGetAllBookingInteractor.Response> GetAll(IGetAllBookingInteractor.Request request);
    }
    public class GetAllBookingInteractor : IGetAllBookingInteractor 
    {
        private readonly IBookingService bookingService;
        public GetAllBookingInteractor(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }
        public async Task<IGetAllBookingInteractor.Response> GetAll(IGetAllBookingInteractor.Request request)
        {
            return await bookingService.GetAll(request);
        }
    }
}
