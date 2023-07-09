using hotel_management_api.APIs.Hotel.DTOs;

namespace hotel_management_api.Business.Interactor.Hotel
{
    public interface IAppovalHotelInteractor
    {
        public class Response
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public HotelDto? HotelDto { get; set; }
        }
        public class ListApprovalHotelResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public List<HotelDto>? HotelDtos { get; set; }
        }
    }
}
