using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Database.Repository;
using hotel_management_api.Utils;
using static Humanizer.In;
using System.IdentityModel.Tokens.Jwt;
using hotel_management_api.Business.Interactor.User;
using hotel_management_api.Database.Model;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using hotel_management_api.APIs.User.DTOs;
using hotel_management_api.APIs.User.UserDTOs;

namespace hotel_management_api.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IJwtUtil jwtUtil;
        private readonly ISendMailUtil sendMailUtil;
        private readonly IConfiguration configuration;
        private readonly IUploadFileUtil uploadFileUtil;
        private readonly IUserRepository userRepository;
        public UserService(
            IJwtUtil _jwtUtil,
            ISendMailUtil _sendMailUtil,
            IConfiguration configuration,
            IUserRepository userRepository,
            IUploadFileUtil uploadFileUtil
            )
        {
            this.jwtUtil = _jwtUtil;
            this.sendMailUtil = _sendMailUtil;
            this.configuration = configuration;
            this.uploadFileUtil = uploadFileUtil;
            this.userRepository = userRepository;
        }
        public async Task<string?> GetUserIdFromToken(string token)
        {
            string userName = jwtUtil.getUserNameFromToken(token);
            if(userName == null)
            {
                return null;
            }
            return (await userRepository.findUserByEmailAsync(userName)).Id;

        }
        public async Task<UserDto?> getDetailAsync(string userName)
        {
            var user = await userRepository.findUserByEmailAsync(userName);
            if (user != null)
            {
                return new UserDto()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age,
                    Avatar = user.Avatar,
                    PhoneNumber = user.PhoneNumber
                };
            }
            return null;
        }
        public async Task<IDeleteUserInteractor.Response> DeleteAsync(IDeleteUserInteractor.Request request)
        {
            string userId = await GetUserIdFromToken(request.token);
            if (userId == null || userId == request.deleteId)
            {
                return new IDeleteUserInteractor.Response()
                {
                    Message = "Delete user failed",
                    Success = false
                };
            }
            var result = await userRepository.DeleteAsync(request.deleteId);
            if (result)
            {
                return new IDeleteUserInteractor.Response()
                {
                    Message = "Delete user success",
                    Success = true
                };
            }
            return new IDeleteUserInteractor.Response()
            {
                Message = "Delete user failed",
                Success = false
            };
        }
        public async Task<IUpdateUserInteractor.Response> UpdateAsync(IUpdateUserInteractor.Request request)
        {
            UpdateUserDto updateUserDto = request.updateUserDto;
            string userId = await GetUserIdFromToken(request.token);
            AppUser user = await userRepository.findUserByEmailAsync(updateUserDto.Email);
            if (user != null && user.Id != userId)
                return new IUpdateUserInteractor.Response()
                {
                    Success = true,
                    Message = "Email already used by another user"
                };
            string? avatar = "";
            AppUser updateUser = await userRepository.FindByIdAsync(userId);
            if (updateUserDto.Avatar != null) { 
                avatar = await uploadFileUtil.UploadAsync(updateUserDto.Avatar);
                updateUser.Avatar = avatar;
            }
            updateUser.Age = updateUserDto.Age;
            updateUser.Email = updateUserDto.Email;
            updateUser.LastName = updateUserDto.LastName;
            updateUser.FirstName = updateUserDto.FirstName;
            updateUser.PhoneNumber = updateUserDto.PhoneNumber;
            var isSuccess = await userRepository.updateUser(updateUser);
            if(isSuccess)
                return new IUpdateUserInteractor.Response()
                {
                    Success = true,
                    Message = "Update user success"
                };
            return new IUpdateUserInteractor.Response()
            {
                Success = false,
                Message = "Update user failed"
            };
        }
        public async Task<IUserLoginInteractor.Response> LoginService(IUserLoginInteractor.Request request)
        {
            var result = await userRepository.LoginRepository(request.loginDto);
            if (result == null)
            {
                return new IUserLoginInteractor.Response(null, null, "login failed", false);
            }
            var li = result.ToList();
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtUtil.GenerateAccessToken(li, request.loginDto.UserName));
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtUtil.GenerateRefreshToken(li, request.loginDto.UserName));
            return new IUserLoginInteractor.Response(accessToken, refreshToken, "login success", true);
        }
        public async Task<IUserSignupInteractor.Response> SignupService(IUserSignupInteractor.Request request)
        {
            var dto = request.dto;
            var userExist = await userRepository.userExist(request.dto.UserName);
            if (userExist == null)
            {
                AppUser user = new AppUser()
                {
                    Email = dto.UserName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = dto.UserName,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };
                var result = await userRepository.createUserAsync(user, dto.Password);
                if (!result.Succeeded)
                    return new IUserSignupInteractor.Response("Can't create user", false);
                if (!await userRepository.roleExist(DbUserRole.User))
                    await userRepository.createRoleAsync(new IdentityRole(DbUserRole.User));
                if (!await userRepository.roleExist(DbUserRole.Admin))
                {
                    await userRepository.createRoleAsync(new IdentityRole(DbUserRole.Admin));
                }
                if (!await userRepository.roleExist(DbUserRole.Owner))
                {
                    await userRepository.createRoleAsync(new IdentityRole(DbUserRole.Owner));
                }
                //role user is role default
                await userRepository.addUserRoleAsync(user, DbUserRole.User);
                foreach (string userRole in request.dto.Role)
                {
                    switch(userRole)
                    {
                        case DbUserRole.Admin:
                            await userRepository.addUserRoleAsync(user, DbUserRole.Admin);
                            break;
                        case DbUserRole.Owner:
                            await userRepository.addUserRoleAsync(user, DbUserRole.Owner);
                            break;
                    }
                }
                return new IUserSignupInteractor.Response("Create user success", true);
            }
            return new IUserSignupInteractor.Response("User is already exists", false);
        }
        public async Task<IFogotPasswordInteractor.Response> fogotPasswordService(IFogotPasswordInteractor.Request request)
        {
            var user = await userRepository.findUserByEmailAsync(request.Email);
            if (user == null)
                return new IFogotPasswordInteractor.Response("Email is not exists", false);
            else if (!jwtUtil.isTokenExpired(user.ResetPasswordToken))
            {
                return new IFogotPasswordInteractor.Response("We already sent to your account! Please try again after 3 minute", false);
            }
            var token = new JwtSecurityTokenHandler().WriteToken(jwtUtil.GenerateResetPasswordApiToken(user.UserName));
            var result = await sendMailUtil.SendMailAsync(user.Email, "Please click to the link bellow to reset your password",
                $"{this.configuration["ApplicationHost"]}/fogot-password/{token}");
            if (result.Success == true) await userRepository.updateResetPasswordTokenAsync(user.UserName, token);
            return new IFogotPasswordInteractor.Response(result.Message, result.Success);
        }
        public async Task<IResetPasswordInteractor.Response> resetPasswordService(IResetPasswordInteractor.Request request)
        {
            string userName = jwtUtil.getUserNameFromToken(request.token);
            bool isSuccess = await userRepository.updatePassword(userName, request.password);
            if (isSuccess)
            {
                return new IResetPasswordInteractor.Response()
                {
                    Message = "update new password success",
                    Success = true
                };
            }
            return new IResetPasswordInteractor.Response()
            {
                Message = "failed orcurr when update password",
                Success = false
            };
        }
        public async Task<IChangePasswordInteractor.Response> changePasswordService(IChangePasswordInteractor.Request request)
        {
            string userId = await GetUserIdFromToken(request.token);
            if (userId == null)
                return new IChangePasswordInteractor.Response()
                {
                    Success = false,
                    Message = "Change password failed"
                };
            bool isSuccess = await userRepository.ChangePassword(userId, request.oldPassword, request.newPassword);
            if(isSuccess) 
                return new IChangePasswordInteractor.Response()
                {
                    Success = true,
                    Message = "Change password success"
                };
            return new IChangePasswordInteractor.Response()
            {
                Success = false,
                Message = "Change password failed"
            };
        }
        public async Task<IBlockAndUnlockUserInteractor.Response> BlockUserAsync(IBlockAndUnlockUserInteractor.Request request)
        {
            string userId = await GetUserIdFromToken(request.token);
            List<string> userRoles = (await userRepository.GetListRoleOfUser(userId)).ToList();
            List<string> blockUuserRoles = (await userRepository.GetListRoleOfUser(request.userId)).ToList();
            if(userRoles.Count == 0 || blockUuserRoles.Count == 0 
                || !userRoles.Contains("owner") || blockUuserRoles.Contains("owner")) 
            {
                return new IBlockAndUnlockUserInteractor.Response()
                {
                    Success = true,
                    Message = "User don't have permission!"
                };
            }
            await userRepository.BlockAsync(request.userId);
            return new IBlockAndUnlockUserInteractor.Response()
            {
                Success = true,
                Message = "Block user success"
            };
        }
        public async Task<IBlockAndUnlockUserInteractor.Response> UnlockUserAsync(IBlockAndUnlockUserInteractor.Request request)
        {
            string userId = await GetUserIdFromToken(request.token);
            List<string> userRoles = (await userRepository.GetListRoleOfUser(userId)).ToList();
            List<string> blockUuserRoles = (await userRepository.GetListRoleOfUser(request.userId)).ToList();
            if (userRoles.Count == 0 || blockUuserRoles.Count == 0
                || !userRoles.Contains("owner"))
            {
                return new IBlockAndUnlockUserInteractor.Response()
                {
                    Success = true,
                    Message = "User don't have permission!"
                };
            }
            await userRepository.UnlockAsync(request.userId);
            return new IBlockAndUnlockUserInteractor.Response()
            {
                Success = true,
                Message = "Unlock user success"
            };
        }
    }
}
