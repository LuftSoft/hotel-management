using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.User
{
    public interface IChangePasswordInteractor
    {
        public class Request
        {
            public string? token ; 
            public string? oldPassword;
            public string? newPassword;
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
        Task<IChangePasswordInteractor.Response> ChangePassword(IChangePasswordInteractor.Request request);
    }
    public class ChangePasswordInteractor: IChangePasswordInteractor
    {
        private readonly IUserService userService;
        public ChangePasswordInteractor(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IChangePasswordInteractor.Response> ChangePassword(IChangePasswordInteractor.Request request)
        {
            return await userService.changePasswordService(request);
        }
    }
}
