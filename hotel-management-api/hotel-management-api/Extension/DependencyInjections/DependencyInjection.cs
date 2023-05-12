using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Business.Interactor.User;
using hotel_management_api.Business.Services;
using hotel_management_api.Database.Repository;

namespace hotel_management_api.Extension.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection RepositoryDependencyInjection( this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
        public static IServiceCollection ServiceDependencyInjection( this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }
        public static IServiceCollection UserInteractorDependencyInjection( this IServiceCollection services)
        {
            services.AddScoped<IUserLoginInteractor, UserLoginInteractor>();
            return services;
        }
    }
}
