using hotel_management_api.APIs.User.DTOs;
using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Business.Interactor.User;
using hotel_management_api.Database.Model;
using hotel_management_api.Utils;
using Humanizer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static hotel_management_api.Extension.Middlewares.IsUserBlockMiddleware;

namespace hotel_management_api.APIs.User
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/user")]
    [TypeFilter(typeof(hotelfilter))]
    public class UserController : ControllerBase
    {
        private readonly IJwtUtil jwtUtil;
        private readonly IConfiguration configuration;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserLoginInteractor userLoginInteractor;
        private readonly IUserSignupInteractor userSignupInteractor;
        private readonly IUpdateUserInteractor updateUserInteractor;
        private readonly IDeleteUserInteractor deleteUserInteractor;
        private readonly IGetAllUserInteractor getAllUserInteractor;
        private readonly IRefreshTokenInteractor refreshTokenInteractor;
        private readonly IFogotPasswordInteractor fogotPasswordInteractor;
        private readonly IGetDetailUserInteractor getDetailUserInteractor;
        private readonly IResetPasswordInteractor resetPasswordInteractor;
        private readonly IAddRoleToUserInteractor addRoleToUserInteractor;
        private readonly IChangePasswordInteractor changePasswordInteractor;
        private readonly IBlockAndUnlockUserInteractor blockAndUnlockUserInteractor;
        public UserController(
            IJwtUtil jwtUtil,
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserLoginInteractor _userLoginInteractor,
            IUpdateUserInteractor updateUserInteractor,
            IDeleteUserInteractor deleteUserInteractor,
            IUserSignupInteractor _userSignupInteractor,
            IGetAllUserInteractor getAllUserInteractor,
            IRefreshTokenInteractor refreshTokenInteractor,
            IGetDetailUserInteractor getDetailUserInteractor,
            IResetPasswordInteractor resetPasswordInteractor,
            IAddRoleToUserInteractor addRoleToUserInteractor,
            IFogotPasswordInteractor _fogotPasswordInteractor,
            IChangePasswordInteractor changePasswordInteractor,
            IBlockAndUnlockUserInteractor blockAndUnlockUserInteractor
            )
        {
            this.jwtUtil = jwtUtil;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.userLoginInteractor = _userLoginInteractor;
            this.updateUserInteractor = updateUserInteractor;
            this.deleteUserInteractor = deleteUserInteractor;
            this.getAllUserInteractor = getAllUserInteractor;
            this.userSignupInteractor = _userSignupInteractor;
            this.refreshTokenInteractor = refreshTokenInteractor;
            this.addRoleToUserInteractor = addRoleToUserInteractor;
            this.getDetailUserInteractor = getDetailUserInteractor;
            this.resetPasswordInteractor = resetPasswordInteractor;
            this.fogotPasswordInteractor = _fogotPasswordInteractor;
            this.changePasswordInteractor = changePasswordInteractor;
            this.blockAndUnlockUserInteractor = blockAndUnlockUserInteractor;
        }
        //GET
        [MapToApiVersion("1.0")]
        [HttpGet()]
        [Authorize("owner")]
        public async Task<IActionResult> GetAll()
        {
            var result = await getAllUserInteractor.GetAllUser();
            return Ok(result);
        }
        [HttpPost("add-role")]
        [Authorize("owner")]
        public async Task<IActionResult> AddRoleToUser(AddRoleDto dto)
        {
            var result = await addRoleToUserInteractor.AddRoleToUser(dto.UserId, dto.RoleName);
            return Ok(result);
        }
        [HttpGet("detail")]
        [Authorize("user")]
        public async Task<IActionResult> Get()
        {
            var result = await getDetailUserInteractor.Get(HttpContext);
            return Ok(result);
        }
        [HttpGet("refresh-token")]
        public async Task<IActionResult> refreshToken()
        {
            string token = HttpContext.Request.Headers.Authorization.FirstOrDefault();
            if(token == null)
            {
                return BadRequest("token is invalid");
            }
            var result = await refreshTokenInteractor.RefreshToken(token);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        //POST
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await userLoginInteractor.Login(new IUserLoginInteractor.Request(dto));

            if(result.Success == false) return Unauthorized();
            return Ok(result);
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignupDto dto)
        {
            var result = await userSignupInteractor.Signup(new IUserSignupInteractor.Request(dto));
            if(result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);  
        }
        [HttpPost("fogot-password")]
        public async Task<IActionResult> fogotpassword([FromBody] FogotPasswordDto dto)
        {   
            var resullt = await fogotPasswordInteractor.fogotPassword(new IFogotPasswordInteractor.Request(dto.Email,dto.Url));
            if(resullt.Success == true) return Ok(resullt);
            return BadRequest(resullt);
        }
        
        //PUT
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDto dto)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null) return Unauthorized();
            var result = await updateUserInteractor.UpdateAsync(new IUpdateUserInteractor.Request()
            {
                token = token,
                updateUserDto = dto
            });
            if (result.Success == true) return Ok(result);
            return BadRequest(result);
        }
        //PATCH
        [Authorize("user")]
        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null)
                return BadRequest("token is null");
            var result = await changePasswordInteractor.ChangePassword(new IChangePasswordInteractor.Request()
            {
                token = token,
                oldPassword = dto.OldPassword,
                newPassword = dto.NewPassword
            });
            if (result.Success == false) return BadRequest(result);
            return Ok(result);
        }
        [HttpPatch("block/{userId}")]
        public async Task<IActionResult> blockUser(string userId)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            var result = await blockAndUnlockUserInteractor.BlockUserAsync(new IBlockAndUnlockUserInteractor.Request()
            {
                token = token,
                userId = userId
            });
            if(result.Success == true) return Ok(result);
            return BadRequest(result);
        }
        [HttpPatch("unlock/{userId}")]
        public async Task<IActionResult> unlockUser(string userId)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            var result = await blockAndUnlockUserInteractor.UnlockUserAsync(new IBlockAndUnlockUserInteractor.Request()
            {
                token = token,
                userId = userId
            });
            if (result.Success == true) return Ok(result);
            return BadRequest(result);
        }
        [HttpPatch("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var result = await resetPasswordInteractor.ResetPassword(new IResetPasswordInteractor.Request()
            {
                token = resetPasswordDto.token,
                password = resetPasswordDto.newPassword
            });
            if (result.Success == true)
                return Ok(result);
            return BadRequest(result);
        }
        //DELETE
        [Authorize("owner")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null) return Unauthorized();
            var result = await deleteUserInteractor.DeleteAsync(new IDeleteUserInteractor.Request()
            {
                token = token,
                deleteId = id
            });
            if (result.Success == true) return Ok(result);
            return BadRequest(result);
        }
    }
}
