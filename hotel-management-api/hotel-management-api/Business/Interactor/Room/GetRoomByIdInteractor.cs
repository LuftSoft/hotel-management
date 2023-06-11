using hotel_management_api.APIs.Room.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Room
{
    public interface IGetRoomByIdInteractor
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public RoomDetailDto RoomDetailDto { get; set; }
        }
        Task<IGetRoomByIdInteractor.Response> GetByIdAsync(int id);
    }
    public class GetRoomByIdInteractor : IGetRoomByIdInteractor
    {
        private readonly IRoomService roomService;
        public GetRoomByIdInteractor(IRoomService roomService)
        {
            this.roomService = roomService;
        }
        public async Task<IGetRoomByIdInteractor.Response> GetByIdAsync(int id)
        {
            return await roomService.GetByIdAsync(id);
        }
    }
}
