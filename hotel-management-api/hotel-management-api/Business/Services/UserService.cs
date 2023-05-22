using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Database.Repository;
using hotel_management_api.Utils;
using static Humanizer.In;
using System.IdentityModel.Tokens.Jwt;
using hotel_management_api.Business.Interactor.User;
using hotel_management_api.Database.Model;
using Humanizer;
using Microsoft.AspNetCore.Identity;

namespace hotel_management_api.Business.Services
{
    public interface IUserService
    {
        Task<IUserLoginInteractor.Response> LoginService(IUserLoginInteractor.Request request);
        Task<IUserSignupInteractor.Response> SignupService(IUserSignupInteractor.Request request);
        Task<IFogotPasswordInteractor.Response> fogotPasswordService(IFogotPasswordInteractor.Request request);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;
        private readonly IJwtUtil jwtUtil;
        private readonly ISendMailUtil sendMailUtil;
        public UserService(
            IUserRepository userRepository,
            IConfiguration configuration,
            IJwtUtil _jwtUtil,
            ISendMailUtil _sendMailUtil
            ) 
        {
            this.userRepository = userRepository;
            this.jwtUtil = _jwtUtil;
            this.sendMailUtil = _sendMailUtil;
            this.configuration = configuration;
        }
        public async Task<IUserLoginInteractor.Response> LoginService(IUserLoginInteractor.Request request)
        {
            var result = (await userRepository.LoginRepository(request.loginDto)).ToList();
            if (result == null)
            {
                return new IUserLoginInteractor.Response(null, null, "login failed", false);
            }
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtUtil.GenerateAccessToken(result, request.loginDto.UserName));
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtUtil.GenerateRefreshToken(result, request.loginDto.UserName));
            return new IUserLoginInteractor.Response(accessToken, refreshToken,"login success", true);
        }
        public async Task<IUserSignupInteractor.Response> SignupService(IUserSignupInteractor.Request request)
        {
            var dto = request.dto;
            var userExist = await userRepository.userExist(request.dto.UserName);
            if (userExist == null)
            {
                AppUser user = new()
                {
                    Email = dto.UserName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = dto.UserName
                };
                var result = await userRepository.createUserAsync(user, dto.Password);
                if (!result.Succeeded)
                    return new IUserSignupInteractor.Response("Can't create user", false);
                Console.WriteLine(DbUserRole.Admin);
                if (!await userRepository.roleExist(DbUserRole.User))
                    await userRepository.createRoleAsync(new IdentityRole(DbUserRole.User));
                
                //if (await userRepository.roleExist(DbUserRole.Admin))
                //{
                //    await userRepository.addUserRoleAsync(user, DbUserRole.User);
                //}
                return new IUserSignupInteractor.Response("", true);
            }
            return  new IUserSignupInteractor.Response("", true);
        }
        public async Task<IFogotPasswordInteractor.Response> fogotPasswordService(IFogotPasswordInteractor.Request request)
        {
            var user = await userRepository.findUserByEmailAsync(request.Email);
            if (user == null)
                return new IFogotPasswordInteractor.Response("Email is not exists", false);
            else if(!jwtUtil.isTokenExpired(user.ResetPasswordToken))
            {
                return new IFogotPasswordInteractor.Response("We already sent to your account! Please try again after 3 minute", false);
            }
            var token = new JwtSecurityTokenHandler().WriteToken(jwtUtil.GenerateResetPasswordApiToken(user.UserName));
            var result = await sendMailUtil.SendMailAsync(user.Email, "Please click to the link bellow to reset your password", 
                $"{this.configuration["ApplicationHost"]}/fogot-password/{token}");
            if (result.Success == true) await userRepository.updateResetPasswordTokenAsync(user.UserName, token);
            return new IFogotPasswordInteractor.Response(result.Message, result.Success);
        }
        public async Task<string> changePasswordService()
        {
            return "";
        }
        public async Task<string> resetPasswordService()
        {
            return "";
        }
    }
}
