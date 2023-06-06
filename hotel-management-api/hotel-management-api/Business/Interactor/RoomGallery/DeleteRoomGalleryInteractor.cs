using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.RoomGallery
{
    public interface IDeleteRoomGalleryInteractor
    {
        public class Request
        {
            public int[] dto { set; get; }
            public string? token { set; get; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
        Task<IDeleteRoomGalleryInteractor.Response> Delete(IDeleteRoomGalleryInteractor.Request request);
    }
    public class DeleteRoomGalleryInteractor : IDeleteRoomGalleryInteractor
    {
        private readonly IRoomGalleryService roomGalleryService;
        public DeleteRoomGalleryInteractor(IRoomGalleryService roomGalleryService)
        {
            this.roomGalleryService = roomGalleryService;
        }
        public async Task<IDeleteRoomGalleryInteractor.Response> Delete(IDeleteRoomGalleryInteractor.Request request)
        {
            return await roomGalleryService.Delete(request);
        }
    }
}
