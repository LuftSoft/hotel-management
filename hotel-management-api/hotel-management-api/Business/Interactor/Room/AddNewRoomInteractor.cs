using hotel_management_api.APIs.Hotel.DTOs;
using hotel_management_api.APIs.Room.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Room
{
    public interface IAddNewRoomInteractor
    {
        public class Request
        {
            public CreateRoomDto dto { set; get; }
            public string token { set; get; }
            public Request(CreateRoomDto dto, string token)
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
        Task<IAddNewRoomInteractor.Response> Create(IAddNewRoomInteractor.Request request);
    }
    public class AddNewRoomInteractor : IAddNewRoomInteractor
    {
        private readonly IRoomService roomService;
        public AddNewRoomInteractor(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        public async Task<IAddNewRoomInteractor.Response> Create(IAddNewRoomInteractor.Request request)
        {
            return await roomService.Create(request);
        }
    }
}
