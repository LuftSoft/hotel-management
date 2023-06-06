using hotel_management_api.APIs.Room.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.RoomGallery
{
    public interface ICreateRoomGalleryInteractor
    {
        public class Request
        {
            public IFormFile[] dto { set; get; }
            public int RoomId { get; set; }
            public string token { set; get; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
        Task<ICreateRoomGalleryInteractor.Response> Create(ICreateRoomGalleryInteractor.Request request);
    }
    public class CreateRoomGalleryInteractor : ICreateRoomGalleryInteractor
    {
        private readonly IRoomGalleryService roomGalleryService;
        public CreateRoomGalleryInteractor(IRoomGalleryService roomGalleryService)
        {
            this.roomGalleryService = roomGalleryService;
        }
        public async Task<ICreateRoomGalleryInteractor.Response> Create(ICreateRoomGalleryInteractor.Request request)
        {
            return await roomGalleryService.Create(request);
        }
    }
}
