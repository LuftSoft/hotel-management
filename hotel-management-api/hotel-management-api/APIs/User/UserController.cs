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
        private readonly IUserLoginInteractor userLoginInteractor;
        private readonly IUserSignupInteractor userSignupInteractor;
        private readonly IFogotPasswordInteractor fogotPasswordInteractor;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IJwtUtil jwtUtil;
        private readonly IConfiguration configuration;
        public UserController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserLoginInteractor _userLoginInteractor,
            IUserSignupInteractor _userSignupInteractor,
            IFogotPasswordInteractor _fogotPasswordInteractor,
            IConfiguration configuration,
            IJwtUtil jwtUtil
            
            ) 
        {
            this.userLoginInteractor = _userLoginInteractor;
            this.userSignupInteractor = _userSignupInteractor;
            this.fogotPasswordInteractor = _fogotPasswordInteractor;
            this.configuration = configuration;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtUtil = jwtUtil;
        }
        //GET
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Authorize("admin")]
        public IActionResult Get() 
        {
            return Ok("Get method");
        }
        [HttpGet("reset-password")]
        public IActionResult ResetPassword([FromQuery] string token)
        {
            if (jwtUtil.getUserIdFromToken(token) == null)
                return BadRequest("bad trip");
            return Ok(jwtUtil.getUserIdFromToken(token));
        }
        [HttpGet("refresh_token")]
        [Authorize(Roles = "admin")]
        public IActionResult testJwt([FromQuery]string token)
        {
            if (jwtUtil.getUserIdFromToken(token) == null)
                return BadRequest("bad trip");
            return Ok(jwtUtil.getUserIdFromToken(token));
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
        //DELETE
        [Authorize("owner")]
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("dedlete method");
        }
    }
}
