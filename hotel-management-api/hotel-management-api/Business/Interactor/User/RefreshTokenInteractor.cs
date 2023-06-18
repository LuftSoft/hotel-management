using hotel_management_api.APIs.User.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.User
{
    public interface IRefreshTokenInteractor
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public string? AccessToken { get; set; }
            public string? RefreshToken { get; set; }
        }
        Task<IRefreshTokenInteractor.Response> RefreshToken(string token);
    }
    public class RefreshTokenInteractor : IRefreshTokenInteractor
    {
        private readonly IUserService userService;
        public RefreshTokenInteractor(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IRefreshTokenInteractor.Response> RefreshToken(string token)
        {
            return await userService.RefreshToken(token);
        }
    }
}
