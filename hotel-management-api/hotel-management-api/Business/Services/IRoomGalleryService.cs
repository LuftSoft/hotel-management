using hotel_management_api.Business.Interactor.RoomGallery;
using hotel_management_api.Database.Repository;

namespace hotel_management_api.Business.Services
{
    public interface IRoomGalleryService
    {
        Task<ICreateRoomGalleryInteractor.Response> Create(ICreateRoomGalleryInteractor.Request request);
        Task<IDeleteRoomGalleryInteractor.Response> Delete(IDeleteRoomGalleryInteractor.Request request);
        Task<IEnumerable<RoomGalleryDto>> GetByRoomId(int roomId);   
    }
}
