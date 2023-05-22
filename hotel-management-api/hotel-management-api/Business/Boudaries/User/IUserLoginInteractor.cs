using hotel_management_api.APIs.User.UserDTOs;

namespace hotel_management_api.Business.Boudaries.User
{
    public interface IUserLoginInteractor
    {
        public class Request {
            public LoginDto? loginDto { set; get; }
            public Request(LoginDto dto) 
            {
                this.loginDto = dto;
            }
        }
        public class Response { 
            public string? AccessToken { set; get; }
            public string? RefreshToken { set; get; }
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public Response(string? accessToken,string? refreshToken, string message, bool success)
            {
                Message = message;
                Success = success;
                AccessToken = accessToken;
                RefreshToken = refreshToken;
            }
        }
        Task<Response> Login(Request request);
    }
}
