using hotel_management_api.APIs.Booking.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Booking
{
    public interface IGetAllBookingByHotelInteractor
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public List<BookingListDto>? BookingList { set; get; }
        }
        Task<IGetAllBookingByHotelInteractor.Response> GetAllByHotelIdAsync(int hotelId);
    }
    public class GetAllBookingByHotelInteractor : IGetAllBookingByHotelInteractor
    {
        private readonly IBookingService _bookingService;
        public GetAllBookingByHotelInteractor(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public async Task<IGetAllBookingByHotelInteractor.Response> GetAllByHotelIdAsync(int hotelId)
        {
            return await _bookingService.GetByHotelIdAsync(hotelId);
        }
    }
}
