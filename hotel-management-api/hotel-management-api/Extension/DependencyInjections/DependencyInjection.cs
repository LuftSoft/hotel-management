using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Business.Interactor.Booking;
using hotel_management_api.Business.Interactor.Hotel;
using hotel_management_api.Business.Interactor.Room;
using hotel_management_api.Business.Interactor.RoomGallery;
using hotel_management_api.Business.Interactor.User;
using hotel_management_api.Business.Services;
using hotel_management_api.Database.Repository;
using hotel_management_api.Utils;
using System.Runtime.CompilerServices;

namespace hotel_management_api.Extension.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection RepositoryDependencyInjection( this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IHotelBenefitRepository, HotelBenefitRepository>();
            services.AddScoped<IHomeletRepository, HomeletRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IProvineRepository, ProvineRepository>();
            services.AddScoped<IRoomGalleryRepository, RoomGalleryRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            return services;
        }
        public static IServiceCollection ServiceDependencyInjection( this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHotelService,HotelService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomGalleryService, RoomGalleryService>();
            return services;
        }
        public static IServiceCollection UtilServiceDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IJwtUtil, JwtUtil>();
            services.AddScoped<ISendMailUtil, SendMailUtil>();
            services.AddScoped<IUploadFileUtil, UploadFileUtil>();
            services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("DefaultLogger"));
            return services;
        }
        public static IServiceCollection UserInteractorDependencyInjection( this IServiceCollection services)
        {
            services.AddScoped<IUserLoginInteractor, UserLoginInteractor>();
            services.AddScoped<IUserSignupInteractor, UserSignupInteractor>();
            services.AddScoped<IFogotPasswordInteractor, FogotPasswordInteractor>();
            services.AddScoped<IResetPasswordInteractor, ResetPasswordInteractor>();
            services.AddScoped<IGetDetailUserInteractor, GetDetailUserInteractor>();
            return services;
        }
        public static IServiceCollection HotelInteractorDependencyInjection( this IServiceCollection services)
        {
            services.AddScoped<IGetListHotelInteractor, GetListHotelInteractor>();
            services.AddScoped<ICreateHotelInteractor, CreateHotelInteractor>();
            services.AddScoped<IUpdateHotelInteractor, UpdateHotelInteractor>();
            services.AddScoped<IDeleteHotelInteractor, DeleteHotelInteractor>();
            return services;
        }
        public static IServiceCollection RoomInteractorDependencyInjection(this IServiceCollection services) 
        {
            services.AddScoped<IGetRoomByHotelIdInteractor, GetRoomByHotelIdInteractor>();
            services.AddScoped<IGetRoomByIdInteractor,  GetRoomByIdInteractor>();   
            services.AddScoped<IAddNewRoomInteractor, AddNewRoomInteractor>();
            services.AddScoped<IUpdateRoomInteractor, UpdateRoomInteractor>();
            services.AddScoped<IDeleteRoomInteractor, DeleteRoomInteractor>();
            return services;
        }
        public static IServiceCollection RoomGalleryInteractorDependencyInjection(this IServiceCollection services) 
        {
            services.AddScoped<ICreateRoomGalleryInteractor, CreateRoomGalleryInteractor>();
            services.AddScoped<IDeleteRoomGalleryInteractor, DeleteRoomGalleryInteractor>();
            return services;
        }
        public static IServiceCollection BookingInteractorDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICreateBookingRoomInteractor, CreateBookingRoomInteractor>();
            services.AddScoped<IUpdateBookingRoomInteractor, UpdateBookingRoomInteractor>();
            services.AddScoped<ICancelBookingRoomInteractor, CancelBookingRoomInteractor>();
            services.AddScoped<IGetAllBookingByUserInteractor, GetAllBookingByUserInteractor>();
            return services;
        }
    }
}
