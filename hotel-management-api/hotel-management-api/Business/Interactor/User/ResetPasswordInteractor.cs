using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Business.Services;
using hotel_management_api.Utils;

namespace hotel_management_api.Business.Interactor.User
{
    public interface IResetPasswordInteractor
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
    }
    public class ResetPasswordInteractor : IResetPasswordInteractor
    {
        private readonly IJwtUtil jwtUtil;
        private readonly IUserService userService;
        public ResetPasswordInteractor(IJwtUtil jwtUtil, IUserService userService) 
        {
            this.jwtUtil = jwtUtil;
            this.userService = userService;
        }
        public async Task<IResetPasswordInteractor.Response> ResetPassword(string? token, string password)
        {
            var userName = jwtUtil.getUserNameFromToken(token);
            return await userService.resetPasswordService(userName, password);
            
        }
    }
}
