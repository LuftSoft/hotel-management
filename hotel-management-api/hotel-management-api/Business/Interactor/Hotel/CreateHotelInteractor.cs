using hotel_management_api.APIs.Hotel.DTOs;
using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Hotel
{   
    public interface ICreateHotelInteractor
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
        Task<ICreateHotelInteractor.Response> Create(ICreateHotelInteractor.Request request);
    }
    public class CreateHotelInteractor : ICreateHotelInteractor
    {
        private readonly IHotelService hotelService;
        public CreateHotelInteractor(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        public async Task<ICreateHotelInteractor.Response> Create(ICreateHotelInteractor.Request request)
        {
            return await hotelService.Create(request);
        }
    }
}
