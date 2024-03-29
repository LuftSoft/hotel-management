﻿using hotel_management_api.Business.Interactor.Hotel;
using hotel_management_api.Database.Model;

namespace hotel_management_api.Business.Services
{
    public interface IHotelService
    {
        Task<IGetDetailHotelInteractor.Response> GetDetail(int hotelId);
        Task<IAppovalHotelInteractor.Response> ApprovalHotel(int hotelId);
        Task<IGetListHotelOfOwnerInteractor.Response> GetHotelOfOwner(string token);
        Task<IAppovalHotelInteractor.ListApprovalHotelResponse> GetListApprovalHotel();
        Task<ICreateHotelInteractor.Response> Create(ICreateHotelInteractor.Request request);
        Task<IUpdateHotelInteractor.Response> Update(IUpdateHotelInteractor.Request request);
        Task<IDeleteHotelInteractor.Response> Delete(IDeleteHotelInteractor.Request request);
        Task<IGetListHotelInteractor.Response> GetPaging(IGetListHotelInteractor.Request request);
        Task<IGetListHotelInteractor.Response> GetAllPaging(IGetListHotelInteractor.Request request);
        Task<IGetListHotelFilterInteractor.Response> GetFilterPaging(IGetListHotelFilterInteractor.Request request);
        public class HotelResponseModel
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public double Star { get; set; }
            public string? Description { get; set; }
            public string? Address { get; set; }
            public string? GoogleLocation { get; set; }
            public string? LogoLink { get; set; }
            public string? Slug { get; set; }
            public DateTime? CreatedDate { get; set; }
            public DateTime? UpdateDate { get; set; }
            public string? HotelCategory{ set; get; }
            public HotelBenefit? HotelBenefit { set; get; }
        }
    }
}
