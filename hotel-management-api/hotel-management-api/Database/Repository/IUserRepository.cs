using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Database.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace hotel_management_api.Database.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<Claim>?> LoginRepository(LoginDto dto);
        Task<AppUser> userExist(string name);
        Task<AppUser> findUserByEmailAsync(string email);
        Task<AppUser?> FindByIdAsync(string id);
        Task<IdentityResult> createUserAsync(AppUser user, string password);
        Task<IdentityResult> addUserRoleAsync(AppUser user, string role);
        //role
        Task<bool> roleExist(string name);
        Task<IdentityResult> createRoleAsync(IdentityRole role);
        Task<bool> updateResetPasswordTokenAsync(string username, string token);
        Task<bool> updatePassword(string userName, string newpassword);
        Task<bool> DeleteAsync(string id);
    }
}
