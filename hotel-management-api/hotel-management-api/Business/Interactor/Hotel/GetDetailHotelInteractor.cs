using hotel_management_api.APIs.Hotel.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Hotel
{
    public interface IGetDetailHotelInteractor
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public HotelDetailDto? hotelDto { get; set; }
            public Response(string message, bool success, HotelDetailDto? hotelDto)
            {
                Message = message;
                Success = success;
                this.hotelDto = hotelDto;
            }
        }
        Task<IGetDetailHotelInteractor.Response> GetDetail(int hotelId);
    }
    public class GetDetailHotelInteractor : IGetDetailHotelInteractor
    {
        private readonly IHotelService hotelService;
        public GetDetailHotelInteractor(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        public async Task<IGetDetailHotelInteractor.Response> GetDetail(int hotelId)
        {
            return await hotelService.GetDetail(hotelId);
        }
    }
}
