using hotel_management_api.APIs.Hotel.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Hotel
{   
    public interface IDeleteHotelInteractor
    {
        public class Request
        {
            public int id { set; get; }
            public string token { set; get; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
        Task<IDeleteHotelInteractor.Response> Delete(IDeleteHotelInteractor.Request request);
    }
    public class DeleteHotelInteractor : IDeleteHotelInteractor
    {
        private readonly IHotelService hotelService;
        public DeleteHotelInteractor(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }
        public async Task<IDeleteHotelInteractor.Response> Delete(IDeleteHotelInteractor.Request request)
        {
            return await hotelService.Delete(request);
        }
    }
}
