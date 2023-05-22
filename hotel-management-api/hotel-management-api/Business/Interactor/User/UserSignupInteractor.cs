using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.User
{
    public interface IUserSignupInteractor
    {
        public class Request
        {
            public SignupDto? dto { set; get; }
            public Request(SignupDto dto)
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
        Task<IUserSignupInteractor.Response> Signup(IUserSignupInteractor.Request request);
    }
    public class UserSignupInteractor : IUserSignupInteractor
    {
        private readonly IUserService userService;
        public UserSignupInteractor(IUserService userService) 
        {
            this.userService = userService; 
        }                  
        public async Task<IUserSignupInteractor.Response> Signup(IUserSignupInteractor.Request request)
        {   
            return await userService.SignupService(request);
        }
    }
}
