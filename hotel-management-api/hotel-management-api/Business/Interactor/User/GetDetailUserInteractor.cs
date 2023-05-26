using hotel_management_api.APIs.User.DTOs;
using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Business.Services;
using hotel_management_api.Utils;

namespace hotel_management_api.Business.Interactor.User
{
    public interface IGetDetailUserInteractor
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public UserDto userDto { get; set; }
        }
        Task<IGetDetailUserInteractor.Response> Get(HttpContext context);
    }
    public class GetDetailUserInteractor : IGetDetailUserInteractor
    {
        private readonly IJwtUtil jwtUtil;
        private readonly IUserService userService;
        public GetDetailUserInteractor(IJwtUtil jwtUtil, IUserService userService)
        {
            this.jwtUtil = jwtUtil;
            this.userService = userService;
        }
        public async Task<IGetDetailUserInteractor.Response> Get(HttpContext context)
        {
            string token = jwtUtil.getTokenFromHeader(context);
            System.Diagnostics.Debug.WriteLine(token);
            var userName = jwtUtil.getUserNameFromToken(token);
            System.Diagnostics.Debug.WriteLine(userName);
            var user = await userService.getDetailAsync(userName);
            if (user == null)
            {
                return new IGetDetailUserInteractor.Response()
                {
                    Message = "failed",
                    Success = false,
                    userDto = null
                };
            }
            return new IGetDetailUserInteractor.Response()
            {
                Message = "success",
                Success = true,
                userDto = user
            };
        }
    }
}
