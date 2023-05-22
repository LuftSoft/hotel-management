using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.User
{
    public class UserLoginInteractor : IUserLoginInteractor
    {
        private readonly IUserService userService;
        public UserLoginInteractor(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IUserLoginInteractor.Response> Login(IUserLoginInteractor.Request request)
        {       
            return await userService.LoginService(request);
        }
    }
}
