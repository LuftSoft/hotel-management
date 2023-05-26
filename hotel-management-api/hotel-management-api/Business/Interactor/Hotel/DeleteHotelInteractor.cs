using hotel_management_api.APIs.Hotel.DTOs;

namespace hotel_management_api.Business.Interactor.Hotel
{   
    public interface IDeleteHotelInteractor
    {
        public class Request
        {
            public HotelDto? dto { set; get; }
            public Request(HotelDto dto)
            {
                this.dto = dto;
            }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public Response(string message, bool success)
            {
                Message = message;
                Success = success;
            }
        }
        Task<IDeleteHotelInteractor.Response> Delete(IDeleteHotelInteractor.Request request);
    }
    public class DeleteHotelInteractor : IDeleteHotelInteractor
    {
        public Task<IDeleteHotelInteractor.Response> Delete(IDeleteHotelInteractor.Request request)
        {
            throw new NotImplementedException();
        }
    }
}
