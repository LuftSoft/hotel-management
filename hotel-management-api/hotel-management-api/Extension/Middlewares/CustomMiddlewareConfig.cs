namespace hotel_management_api.Extension.Middlewares
{
    public static class CustomMiddlewareConfig
    {
        public static IEndpointConventionBuilder UserMyCustomMiddleware(this IEndpointConventionBuilder app)
        {
            
            return app;
        }
    }
}
