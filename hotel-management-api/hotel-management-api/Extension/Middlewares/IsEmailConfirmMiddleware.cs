using hotel_management_api.Business.Services;
using hotel_management_api.Utils;

namespace hotel_management_api.Extension.Middlewares
{
    public class IsEmailConfirmMiddleware
    {
        private readonly RequestDelegate _next;

        public IsEmailConfirmMiddleware(
            RequestDelegate next
            )
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("is email confirmed middlware work!");
            await _next(context);
        }
    }
    public static class UserMyCustomMiddlewareExrension
    {
        public static IApplicationBuilder UserMyCustomMiddleware(this IApplicationBuilder builder) 
        {
            builder.UseMiddleware<IsEmailConfirmMiddleware>();
            return builder;
        }
    }
}