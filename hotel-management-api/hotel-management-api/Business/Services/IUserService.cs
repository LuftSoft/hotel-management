using hotel_management_api.APIs.User.DTOs;
using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Business.Interactor.User;
using hotel_management_api.Database.Model;

namespace hotel_management_api.Business.Services
{
    public interface IUserService
    {
        Task<AppUser> FindByIdAsync(string userId);
        Task<UserDto?> getDetailAsync(string userId);
        Task<string?> GetUserIdFromToken(string token);
        Task<IUserLoginInteractor.Response> LoginService(IUserLoginInteractor.Request request);
        Task<IDeleteUserInteractor.Response> DeleteAsync(IDeleteUserInteractor.Request request);
        Task<IUpdateUserInteractor.Response> UpdateAsync(IUpdateUserInteractor.Request request);
        Task<IUserSignupInteractor.Response> SignupService(IUserSignupInteractor.Request request);
        Task<IFogotPasswordInteractor.Response> fogotPasswordService(IFogotPasswordInteractor.Request request);
        Task<IResetPasswordInteractor.Response> resetPasswordService(IResetPasswordInteractor.Request request);
        Task<IChangePasswordInteractor.Response> changePasswordService(IChangePasswordInteractor.Request request);
        Task<IBlockAndUnlockUserInteractor.Response> BlockUserAsync(IBlockAndUnlockUserInteractor.Request request);
        Task<IBlockAndUnlockUserInteractor.Response> UnlockUserAsync(IBlockAndUnlockUserInteractor.Request request);
    }
}
