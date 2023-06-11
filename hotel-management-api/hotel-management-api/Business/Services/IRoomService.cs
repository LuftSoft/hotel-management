using hotel_management_api.APIs.Room.DTOs;
using hotel_management_api.Business.Interactor.Room;

namespace hotel_management_api.Business.Services
{
    public interface IRoomService
    {
        Task<string> GetUserId(int roomId);
        Task<IGetRoomByIdInteractor.Response> GetByIdAsync(int id);
        Task<IAddNewRoomInteractor.Response> Create(IAddNewRoomInteractor.Request request);
        Task<IUpdateRoomInteractor.Response> Update(IUpdateRoomInteractor.Request request);
        Task<IDeleteRoomInteractor.Response> Delete(IDeleteRoomInteractor.Request request);
    }
}
