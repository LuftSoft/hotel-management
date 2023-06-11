﻿using hotel_management_api.APIs.User.DTOs;
using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Database.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace hotel_management_api.Database.Repository
{   
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
        public async Task<bool> DeleteAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);   
            if(user == null)
            {
                return false;
            }
            appDbContext.Users.Remove(user);
            await appDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> roleExist(string name)
        {
            return await roleManager.RoleExistsAsync(name);
        }
        public async Task<bool> updateUser(AppUser user)
        {
            try
            {
                appDbContext.Users.Update(user);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> BlockAsync(string userId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user == null) return false;
                user.IsBlock = true;
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<AppUser> userExist(string name)
        {
            return await userManager.FindByNameAsync(name);
        }
        public async Task<bool> UnlockAsync(string userId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user == null) return false;
                user.IsBlock = false;
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<AppUser?> FindByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }
        public async Task<AppUser> findUserByEmailAsync(string email) 
        {
            return await userManager.FindByEmailAsync(email);
        }
        public async Task<IdentityResult> createRoleAsync(IdentityRole role)
        {
            return await roleManager.CreateAsync(role);
        }
        public async Task<IEnumerable<Claim>?> LoginRepository(LoginDto dto)
        {
            try
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"error when create user repository: {ex.Message}");
                return null;
            }
        }
        public async Task<IEnumerable<string>> GetListRoleOfUser(string userId)
        {
            var userRole = appDbContext.UserRoles.AsNoTracking().Where(r => r.UserId ==  userId).Select(r => r.RoleId).ToArray();
            return await appDbContext.Roles.Where(r => userRole.Contains(r.Id)).Select(r => r.Id).ToListAsync();
        }
        public async Task<bool> updatePassword(string userName, string newpassword)
        {
            var user = await appDbContext.Users.Where(u => u.Email.Equals(userName)).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }
            string newpass = userManager.PasswordHasher.HashPassword(user,newpassword);
            user.PasswordHash = newpass;
            await appDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<IdentityResult> addUserRoleAsync(AppUser user, string role) 
        {
            return await userManager.AddToRoleAsync(user, role);
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
        public async Task<bool> ChangePassword(string userId, string oldPassword,string newpassword)
        {
            try {
                var user = await appDbContext.Users.Where(u => u.Id.Equals(userId)).FirstOrDefaultAsync();
                if (user == null)
                {
                    return false;
                }
                var result = userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash,oldPassword);
                
                if (result != PasswordVerificationResult.Success)
                    return false;
                string newpass = userManager.PasswordHasher.HashPassword(user, newpassword);
                user.PasswordHash = newpass;
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
