namespace hotel_management_api.Business.Interactor.Room
{
    public interface IGetRoomByHotelIdInteractor
    {
        public class Request
        {
            public int id { set; get; }
            public string token { set; get; }
            public Request(int id, string token)
            {
                this.id = id;
                this.token = token;
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
    }
    public class GetRoomByHotelIdInteractor : IGetRoomByHotelIdInteractor
    {
    }
}
