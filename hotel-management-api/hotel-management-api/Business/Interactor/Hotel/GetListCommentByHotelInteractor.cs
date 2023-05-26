using hotel_management_api.APIs.Hotel.DTOs;

namespace hotel_management_api.Business.Interactor.Hotel
{
    public interface IGetListCommentByHotelInteractor
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
        Task<IGetListCommentByHotelInteractor.Response> Update(IGetListCommentByHotelInteractor.Request request);
    }
    public class GetListCommentByHotelInteractor : IGetListCommentByHotelInteractor
    {
        public Task<IGetListCommentByHotelInteractor.Response> Update(IGetListCommentByHotelInteractor.Request request)
        {
            throw new NotImplementedException();
        }
    }
}
