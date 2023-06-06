using hotel_management_api.APIs.Room.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Room
{
    public interface IUpdateRoomInteractor
    {
        public class Request
        {
            public string token { set; get; }
            public RoomUpdateDto roomUpdateDto { set; get; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
        Task<IUpdateRoomInteractor.Response> Update(IUpdateRoomInteractor.Request request);
    }
    public class UpdateRoomInteractor : IUpdateRoomInteractor
    {
        private readonly IRoomService roomService;
        public UpdateRoomInteractor(IRoomService roomService)
        {
            this.roomService = roomService;
        }  
        public async Task<IUpdateRoomInteractor.Response> Update(IUpdateRoomInteractor.Request request) 
        {
            return await roomService.Update(request);
        }
    }
}
