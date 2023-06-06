using hotel_management_api.Business.Interactor.Booking;

namespace hotel_management_api.Business.Services
{
    public interface IBookingService
    {
        Task<ICreateBookingRoomInteractor.Response> Create(ICreateBookingRoomInteractor.Request request);
        Task<IGetAllBookingByUserInteractor.Response> GetByUserId(IGetAllBookingByUserInteractor.Request request);
    }
}
