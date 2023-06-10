using hotel_management_api.Business.Services;
using hotel_management_api.Database.Repository;
using hotel_management_api.Utils;

namespace hotel_management_api.Extension.Middlewares
{
    public class IsUserBlockMiddleware
    {
        private readonly IJwtUtil jwtUtil;
        private readonly IUserService userService;
        private readonly IUserRepository userRepository;
        private readonly RequestDelegate _next;
        public IsUserBlockMiddleware(
            IJwtUtil jwtUtil,
            IUserService userService,
            IUserRepository userRepository,
            RequestDelegate requestDelegate
            )
        {
            this.jwtUtil = jwtUtil;
            this.userService = userService;
            this.userRepository = userRepository;
            this._next = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string id = await userService.GetUserIdFromToken(jwtUtil.getTokenFromHeader(context));
            if (id == null)
                return;
            var user = await userRepository.FindByIdAsync(id);
            if (user.IsBlock)
            {
                return;
            }
            await _next(context);
        }
    }
}
