using hotel_management_api.APIs.Hotel.DTOs;
using hotel_management_api.Business.Services;
using hotel_management_api.Database.Model;

namespace hotel_management_api.Business.Interactor.Hotel
{   
    public interface IGetListHotelInteractor
    {
        public class Request
        {   
            public int pageIndex { set; get; }
            public int pageSize { set; get; }
            public GetListHotelFilterDto? dto { set; get; }
        }
        public class Response
        {
            public List<HotelDto>? Hotels { set; get; }
            public List<HotelCategory>? Categories { set; get; }
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public int TotalPage { get; set; }
            public int PageIndex { get; set; }
        }
        Task<IGetListHotelInteractor.Response> GetAsync(IGetListHotelInteractor.Request request);
        Task<IGetListHotelInteractor.Response> GetAllAsync(IGetListHotelInteractor.Request request);
    }
    public class GetListHotelInteractor : IGetListHotelInteractor
    {
        private readonly IHotelService hotelService;
        public GetListHotelInteractor(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        public async Task<IGetListHotelInteractor.Response> GetAsync(IGetListHotelInteractor.Request request)
        {
            return await hotelService.GetPaging(request);
        }
        public async Task<IGetListHotelInteractor.Response> GetAllAsync(IGetListHotelInteractor.Request request)
        {
            return await hotelService.GetAllPaging(request);
        }
    }
}
