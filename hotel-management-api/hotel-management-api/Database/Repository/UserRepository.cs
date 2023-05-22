using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Database.Model;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace hotel_management_api.Database.Repository
{   
    public interface IUserRepository
    {
        Task<IEnumerable<Claim>?> LoginRepository(LoginDto dto);
        Task<AppUser> userExist(string name);
        Task<AppUser> findUserByEmailAsync(string email);
        Task<IdentityResult> createUserAsync(AppUser user, string password);
        Task<IdentityResult> addUserRoleAsync(AppUser user, string role);
        //role
        Task<bool> roleExist(string name);
        Task<IdentityResult> createRoleAsync(IdentityRole role);
        Task<bool> updateResetPasswordTokenAsync(string username, string token);
    }


    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext appDbContext;
        public UserRepository(
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            AppDbContext appDbContext
            )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.appDbContext = appDbContext;
        }
        //
        public async Task<AppUser> userExist(string name)
        {
            return await userManager.FindByNameAsync(name);
        }
        public async Task<AppUser> findUserByEmailAsync(string email) 
        {
            return await userManager.FindByEmailAsync(email);
        }
        public async Task<IdentityResult> createUserAsync(AppUser user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }
        public async Task<bool> updateResetPasswordTokenAsync(string username, string token)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return false;
            };
            user.ResetPasswordToken = token;
            var result = await appDbContext.SaveChangesAsync();
            return result > 0;
        }
        public async Task<IdentityResult> addUserRoleAsync(AppUser user, string role) 
        {
            return await userManager.AddToRoleAsync(user, role);
        }
        //
        public async Task<bool> roleExist(string name)
        {
            return await roleManager.RoleExistsAsync(name);
        }
        public async Task<IdentityResult> createRoleAsync(IdentityRole role)
        {
            return await roleManager.CreateAsync(role);
        }
        //
        public async Task<IEnumerable<Claim>?> LoginRepository(LoginDto dto)
        {
            var user = await userManager.FindByNameAsync(dto.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, dto.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                return authClaims;
            }
            return null;
        }
        public async Task<bool> updateUser()
        {
            return true;
        }
    }
}
