using hotel_management_api.APIs.Hotel.DTOs;
using hotel_management_api.Business.Services;
using hotel_management_api.Database.Model;

namespace hotel_management_api.Business.Interactor.Hotel
{
    public interface IGetListHotelFilterInteractor
    {
        public class Request
        {
            public int pageIndex { set; get; }
            public int pageSize { set; get; }
            public GetListHotelFilterDto? dto { set; get; }
            public HotelFilterDto? filterDto { set; get; }
        }
        public class Response
        {
            public List<HotelDto>? Hotels { set; get; }
            public List<HotelCategory>? Categories { set; get; }
            public string? Message { get; set; }
            public bool Success { get; set; }
            public int TotalPage { get; set; }
            public int PageIndex { get; set; }
        }
        Task<IGetListHotelFilterInteractor.Response> GetFilterPaging(IGetListHotelFilterInteractor.Request request);
    }
    public class GetListHotelFilterInteractor : IGetListHotelFilterInteractor
    {
        private readonly IHotelService hotelService;
        public GetListHotelFilterInteractor (IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }
        public async Task<IGetListHotelFilterInteractor.Response> GetFilterPaging(IGetListHotelFilterInteractor.Request request)
        {
            return await hotelService.GetFilterPaging(request);
        }
    }
}
