using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Business.Interactor.Booking;
using hotel_management_api.Business.Interactor.Comment;
using hotel_management_api.Business.Interactor.Hotel;
using hotel_management_api.Business.Interactor.Location;
using hotel_management_api.Business.Interactor.Room;
using hotel_management_api.Business.Interactor.RoomGallery;
using hotel_management_api.Business.Interactor.User;
using hotel_management_api.Business.Services;
using hotel_management_api.Database.Repository;
using hotel_management_api.Utils;
using System.Runtime.CompilerServices;
using static hotel_management_api.Extension.Middlewares.IsUserBlockMiddleware;

namespace hotel_management_api.Extension.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection RepositoryDependencyInjection( this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IHomeletRepository, HomeletRepository>();
            services.AddScoped<IProvineRepository, ProvineRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IRoomGalleryRepository, RoomGalleryRepository>();
            services.AddScoped<IHotelBenefitRepository, HotelBenefitRepository>();
            services.AddScoped<IHotelCategoryRepository, HotelCategoryRepository>();
            return services;
        }
        public static IServiceCollection ServiceDependencyInjection( this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IHotelService,HotelService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ILocationService, LocationService>();
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
            services.AddScoped<IUpdateUserInteractor, UpdateUserInteractor>();
            services.AddScoped<IDeleteUserInteractor, DeleteUserInteractor>();
            services.AddScoped<IGetAllUserInteractor, GetAllUserInteractor>();
            services.AddScoped<IRefreshTokenInteractor, RefreshTokenInteractor>();
            services.AddScoped<IAddRoleToUserInteractor, AddRoleToUserInteractor>();
            services.AddScoped<IFogotPasswordInteractor, FogotPasswordInteractor>();
            services.AddScoped<IResetPasswordInteractor, ResetPasswordInteractor>();
            services.AddScoped<IGetDetailUserInteractor, GetDetailUserInteractor>();
            services.AddScoped<IResetPasswordInteractor, ResetPasswordInteractor>();
            services.AddScoped<IChangePasswordInteractor, ChangePasswordInteractor>();
            services.AddScoped<IBlockAndUnlockUserInteractor, BlockAndUnlockUserInteractor>();
            return services;
        }
        public static IServiceCollection HotelInteractorDependencyInjection( this IServiceCollection services)
        {
            services.AddScoped<ICreateHotelInteractor, CreateHotelInteractor>();
            services.AddScoped<IUpdateHotelInteractor, UpdateHotelInteractor>();
            services.AddScoped<IDeleteHotelInteractor, DeleteHotelInteractor>();
            services.AddScoped<IGetListHotelInteractor, GetListHotelInteractor>();
            services.AddScoped<IGetDetailHotelInteractor, GetDetailHotelInteractor>();
            services.AddScoped<IGetListHotelFilterInteractor, GetListHotelFilterInteractor>();
            return services;
        }
        public static IServiceCollection RoomInteractorDependencyInjection(this IServiceCollection services) 
        {
            services.AddScoped<IAddNewRoomInteractor, AddNewRoomInteractor>();
            services.AddScoped<IUpdateRoomInteractor, UpdateRoomInteractor>();
            services.AddScoped<IDeleteRoomInteractor, DeleteRoomInteractor>();
            services.AddScoped<IGetRoomByIdInteractor,  GetRoomByIdInteractor>();   
            services.AddScoped<IGetRoomByHotelIdInteractor, GetRoomByHotelIdInteractor>();
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
            services.AddScoped<IGetAllBookingInteractor, GetAllBookingInteractor>();
            services.AddScoped<ICreateBookingRoomInteractor, CreateBookingRoomInteractor>();
            services.AddScoped<IUpdateBookingRoomInteractor, UpdateBookingRoomInteractor>();
            services.AddScoped<ICancelBookingRoomInteractor, CancelBookingRoomInteractor>();
            services.AddScoped<IGetAllBookingByUserInteractor, GetAllBookingByUserInteractor>();
            return services;
        }
        public static IServiceCollection CommentInteractorDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICreateCommentInteractor, CreateCommentInteractor>();
            services.AddScoped<IUpdateCommentInteractor, UpdateCommentInteractor>();
            services.AddScoped<IDeleteCommentInteractor, DeleteCommentInteractor>();
            return services;
        }
        public static IServiceCollection LocationInteractorDependency(this IServiceCollection services)
        {
            services.AddScoped<IGetHomeletInteractor, GetHomeletInteractor>();
            services.AddScoped<IGetDistrictInteractor, GetDistrictInteractor>();
            services.AddScoped<IGetProvineInteractor, GetProvineInteractor>();
            services.AddScoped<hotelfilter>();
            return services;
        }
    }
}
