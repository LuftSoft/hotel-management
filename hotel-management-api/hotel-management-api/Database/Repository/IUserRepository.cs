using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Database.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace hotel_management_api.Database.Repository
{
    public interface IUserRepository
    {
        Task<bool> roleExist(string name);
        Task<bool> DeleteAsync(string id);
        Task<bool> updateUser(AppUser user);
        Task<AppUser> userExist(string name);
        Task<bool> BlockAsync(string userId);
        Task<bool> UnlockAsync(string userId);
        Task<AppUser?> FindByIdAsync(string id);
        Task<AppUser> findUserByEmailAsync(string email);
        Task<IdentityResult> createRoleAsync(IdentityRole role);
        Task<IEnumerable<Claim>?> LoginRepository(LoginDto dto);
        Task<IEnumerable<string>> GetListRoleOfUser(string userId);
        Task<bool> updatePassword(string userName, string newpassword);
        Task<IdentityResult> addUserRoleAsync(AppUser user, string role);
        Task<IdentityResult> createUserAsync(AppUser user, string password);
        Task<bool> updateResetPasswordTokenAsync(string username, string token);
        Task<bool> ChangePassword(string userName, string oldPassword, string newpassword);
    }
}
