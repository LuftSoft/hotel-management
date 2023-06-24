using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.User
{
    public interface IFogotPasswordInteractor
    {
        public class Request
        {
            public string? Email { get; set; }
            public string? Url { get; set; }
            public Request(string? email, string? url)
            {
                Email = email;
                Url = url;
            }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public Response(string? message, bool? success)
            {
                Message = message;
                Success = success;
            }
        }
        Task<IFogotPasswordInteractor.Response> fogotPassword(IFogotPasswordInteractor.Request request);
    }
    public class FogotPasswordInteractor : IFogotPasswordInteractor
    {
        private readonly IUserService userService;
        public FogotPasswordInteractor(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IFogotPasswordInteractor.Response> fogotPassword(IFogotPasswordInteractor.Request request) 
        {
            return await userService.fogotPasswordService(request);
        }
    }
}
