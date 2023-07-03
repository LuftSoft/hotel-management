using hotel_management_api.APIs.Hotel.DTOs;
using hotel_management_api.Business.Services;
using hotel_management_api.Database.Model;

namespace hotel_management_api.Business.Interactor.Hotel
{
    public interface IGetListHotelOfOwnerInteractor
    {
        public class Response
        {
            public List<HotelDto>? Hotels { set; get; }
            public List<HotelCategory>? Categories { set; get; }
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
        Task<IGetListHotelOfOwnerInteractor.Response> GetHotelOfOwner(string token);
    }
    public class GetListHotelOfOwnerInteractor : IGetListHotelOfOwnerInteractor
    {
        private readonly IHotelService hotelService;
        public GetListHotelOfOwnerInteractor(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }
        public async Task<IGetListHotelOfOwnerInteractor.Response> GetHotelOfOwner(string token)
        {
            return await hotelService.GetHotelOfOwner(token);
        }
    }
}
