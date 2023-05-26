using hotel_management_api.APIs.Hotel.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Hotel
{
    public interface IUpdateHotelInteractor
    {
        public class Request
        {
            public HotelUpdateDto? dto { set; get; }
            public string token { set; get; }
            public Request(HotelUpdateDto dto, string token)
            {
                this.dto = dto;
                this.token = token;
            }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public Response(string message, bool success)
            {
                Message = message;
                Success = success;
            }
        }
        Task<IUpdateHotelInteractor.Response> Update(IUpdateHotelInteractor.Request request);
    }
    public class UpdateHotelInteractor : IUpdateHotelInteractor
    {
        private readonly IHotelService hotelService;
        public UpdateHotelInteractor(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        public async Task<IUpdateHotelInteractor.Response> Update(IUpdateHotelInteractor.Request request)
        {
            return await hotelService.Update(request);
        }
    }
}
