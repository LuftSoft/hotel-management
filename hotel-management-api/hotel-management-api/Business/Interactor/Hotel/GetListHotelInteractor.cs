using hotel_management_api.APIs.Hotel.DTOs;

namespace hotel_management_api.Business.Interactor.Hotel
{   
    public interface IGetListHotelInteractor
    {
        public class Request
        {   
            int pageIndex { set; get; }
            int pageSize { set; get; }
            public GetListHotelFilterDto? dto { set; get; }
            public Request(GetListHotelFilterDto dto, int pageIndex, int pageSize)
            {
                this.dto = dto;
                this.pageIndex = pageIndex;
                this.pageSize = pageSize;
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
        Task<IGetListHotelInteractor.Response> Update(IGetListHotelInteractor.Request request);
    }
    public class GetListHotelInteractor : IGetListHotelInteractor
    {
        public Task<IGetListHotelInteractor.Response> Update(IGetListHotelInteractor.Request request)
        {
            throw new NotImplementedException();
        }
    }
}
