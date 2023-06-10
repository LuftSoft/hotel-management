using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Business.Interactor.User;
using hotel_management_api.Database.Model;
using hotel_management_api.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hotel_management_api.APIs.User
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/user")]
    public class UserController : ControllerBase
    {
        private readonly IJwtUtil jwtUtil;
        private readonly IConfiguration configuration;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserLoginInteractor userLoginInteractor;
        private readonly IUserSignupInteractor userSignupInteractor;
        private readonly IFogotPasswordInteractor fogotPasswordInteractor;
        private readonly IGetDetailUserInteractor getDetailUserInteractor;
        private readonly IBlockAndUnlockUserInteractor blockAndUnlockUserInteractor;
        public UserController(
            IJwtUtil jwtUtil,
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserLoginInteractor _userLoginInteractor,
            IUserSignupInteractor _userSignupInteractor,
            IGetDetailUserInteractor getDetailUserInteractor,
            IFogotPasswordInteractor _fogotPasswordInteractor,
            IBlockAndUnlockUserInteractor blockAndUnlockUserInteractor
            ) 
        {
            this.jwtUtil = jwtUtil;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.userLoginInteractor = _userLoginInteractor;
            this.userSignupInteractor = _userSignupInteractor;
            this.getDetailUserInteractor = getDetailUserInteractor;
            this.fogotPasswordInteractor = _fogotPasswordInteractor;
            this.blockAndUnlockUserInteractor = blockAndUnlockUserInteractor;
        }
        //GET
        [MapToApiVersion("1.0")]
        [HttpGet("detail")]
        [Authorize]
        public async Task<IActionResult> Get() 
        {
            var result = await getDetailUserInteractor.Get(HttpContext);
            return Ok(result);
        }
        [HttpGet("reset-password")]
        public IActionResult ResetPassword([FromQuery] string token)
        {
            if (jwtUtil.getUserNameFromToken(token) == null)
                return BadRequest("bad trip");
            return Ok(jwtUtil.getUserNameFromToken(token));
        }
        [HttpGet("refresh_token")]
        [Authorize(Roles = "admin")]
        public IActionResult testJwt([FromQuery]string token)
        {
            if (jwtUtil.getUserNameFromToken(token) == null)
                return BadRequest("bad trip");
            return Ok(jwtUtil.getUserNameFromToken(token));
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
            var resullt = await fogotPasswordInteractor.fogotPassword(new IFogotPasswordInteractor.Request(dto.Email));
            if(resullt.Success == true) return Ok(resullt);
            return BadRequest(resullt);
        }
        
        //PUT
        [HttpPut]
        public IActionResult UpdateUser()
        {
            return Ok("put method");
        }
        //PATCH
        [Authorize("user")]
        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword()
        {
            var ListAuthorize = HttpContext.Request.Headers.Authorization.ToList();
            ListAuthorize.ForEach(item => Console.WriteLine(item));
            var Authorization = HttpContext.GetTokenAsync("Bearer Token");
            if (Authorization == null) return BadRequest("tooken is null");
            return Ok(Authorization);
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
        [HttpPost("unlock/{userId}")]
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
        //DELETE
        [Authorize("owner")]
        [HttpDelete]
        public IActionResult Delete(string userId)
        {
            return Ok("dedlete method");
        }
    }
}
