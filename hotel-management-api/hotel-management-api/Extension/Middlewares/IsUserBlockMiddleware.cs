using hotel_management_api.Business.Services;
using hotel_management_api.Database;
using hotel_management_api.Database.Repository;
using hotel_management_api.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace hotel_management_api.Extension.Middlewares
{
    public class IsUserBlockMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppDbContext appDbContext;
        public IsUserBlockMiddleware(
            RequestDelegate next
            )
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserService userService, IJwtUtil jwtUtil)
        {
            
            try
            {
                string id = await userService.GetUserIdFromToken(jwtUtil.getTokenFromHeader(context));
                if (id == null)
                {
                    context.Response.StatusCode =(int) HttpStatusCode.NotFound;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("token is null");
                    return;
                }
                var user = await userService.FindByIdAsync(id);
                if (user == null || user.IsBlock)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("your account is blocked");
                    return;
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                await _next(context);
            }
        }

        public class hotelfilter : Attribute,IAsyncActionFilter
        {
            private readonly IUserService userService;
            private readonly IJwtUtil jwtUtil;
            public hotelfilter(IUserService userService, IJwtUtil jwtUtil)
            {
                this.userService = userService;
                this.jwtUtil = jwtUtil;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                try
                {
                    var id = await userService.GetUserIdFromToken(jwtUtil.getTokenFromHeader(context.HttpContext));
                    if (id == null)
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        context.HttpContext.Response.ContentType = "text/plain";
                        context.HttpContext.Response.WriteAsync("token is null");
                        return;
                    }
                    var user = await(userService.FindByIdAsync(id));
                    if (user == null || user.IsBlock)
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        context.HttpContext.Response.ContentType = "text/plain";
                        await context.HttpContext.Response.WriteAsync("your account is blocked");
                        return;
                    }
                    await next();
                }
                catch (Exception ex)
                {
                    await next();
                }
            }
        }
    }
}
