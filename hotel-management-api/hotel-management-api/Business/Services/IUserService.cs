using hotel_management_api.APIs.User.DTOs;
using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Business.Interactor.User;

namespace hotel_management_api.Business.Services
{
    public interface IUserService
    {
        Task<UserDto?> getDetailAsync(string userId);
        Task<string?> GetUserIdFromToken(string token);
        Task<IDeleteUserInteractor.Response> DeleteAsync(string userId);
        Task<IUserLoginInteractor.Response> LoginService(IUserLoginInteractor.Request request);
        Task<IUserSignupInteractor.Response> SignupService(IUserSignupInteractor.Request request);
        Task<IResetPasswordInteractor.Response> resetPasswordService(string username, string newpass);
        Task<IFogotPasswordInteractor.Response> fogotPasswordService(IFogotPasswordInteractor.Request request);
        Task<IBlockAndUnlockUserInteractor.Response> BlockUserAsync(IBlockAndUnlockUserInteractor.Request request);
        Task<IBlockAndUnlockUserInteractor.Response> UnlockUserAsync(IBlockAndUnlockUserInteractor.Request request);
    }
}
