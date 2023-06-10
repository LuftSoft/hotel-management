﻿using hotel_management_api.Business.Interactor.Booking;

namespace hotel_management_api.Business.Services
{
    public interface IBookingService
    {
        Task<ICreateBookingRoomInteractor.Response> CreateAsync(ICreateBookingRoomInteractor.Request request);
        Task<IGetAllBookingByUserInteractor.Response> GetByUserIdAsync(IGetAllBookingByUserInteractor.Request request);
        Task<IUpdateBookingRoomInteractor.Response> UpdateAsync(IUpdateBookingRoomInteractor.Request request);
        Task<ICancelBookingRoomInteractor.Response> CancelAsync(ICancelBookingRoomInteractor.Request request);
    }
}
