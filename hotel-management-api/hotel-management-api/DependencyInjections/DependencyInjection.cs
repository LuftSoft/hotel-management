using hotel_management_api.Business.Services;

namespace hotel_management_api.DependencyInjections
{
    public class DependencyInjection
    {
        public static IServiceCollection ServiceDependencyInjection(IServiceCollection services)
        {   
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
