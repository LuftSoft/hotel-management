using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Business.Services;
using hotel_management_api.Utils;

namespace hotel_management_api.Business.Interactor.User
{
    public interface IResetPasswordInteractor
    {
        public class Request
        {
            public string token { get; set; }
            public string password { get; set; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
        Task<IResetPasswordInteractor.Response> ResetPassword(IResetPasswordInteractor.Request request);
    }
    public class ResetPasswordInteractor : IResetPasswordInteractor
    {
        private readonly IUserService userService;
        public ResetPasswordInteractor( IUserService userService) 
        {
            this.userService = userService;
        }
        public async Task<IResetPasswordInteractor.Response> ResetPassword(IResetPasswordInteractor.Request request)
        {
            return await userService.resetPasswordService(request);
        }
    }
}
