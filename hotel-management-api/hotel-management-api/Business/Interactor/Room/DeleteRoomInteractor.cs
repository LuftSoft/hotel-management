using hotel_management_api.APIs.Room.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Room
{
    public interface IDeleteRoomInteractor
    {
        public class Request
        {
            public int id { set; get; }
            public string token { set; get; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
        Task<IDeleteRoomInteractor.Response> Delete(IDeleteRoomInteractor.Request request);
    }
    public class DeleteRoomInteractor : IDeleteRoomInteractor
    {
        private readonly IRoomService roomService;
        public DeleteRoomInteractor(IRoomService roomService)
        {
            this.roomService = roomService;
        }
        public async Task<IDeleteRoomInteractor.Response> Delete(IDeleteRoomInteractor.Request request)
        {
            return await roomService.Delete(request);
        }
    }
}
