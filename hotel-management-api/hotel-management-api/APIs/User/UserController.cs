using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Database.Model;
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
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        public UserController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserLoginInteractor _userLoginInteractor,
            IConfiguration configuration
            ) 
        {
            this.userLoginInteractor = _userLoginInteractor;
            this.configuration = configuration;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        //GET
        [MapToApiVersion("1.0")]
        [HttpGet]
        public IActionResult Get() 
        {
            return Ok("Get method");
        }
        //
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfig:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWTConfig:ValidIssuer"],
                audience: configuration["JWTConfig:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        //POST
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
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

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignupDto dto)
        {
            var userExist = await userManager.FindByNameAsync(dto.UserName);
            if(userExist == null)
            {
                AppUser user = new()
                {   
                    Email = dto.UserName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = dto.UserName
                };
                var result = await userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                    return BadRequest("Can't create user");
                Console.WriteLine(DbUserRole.Admin);
                if (!await roleManager.RoleExistsAsync(DbUserRole.Admin))
                    await roleManager.CreateAsync(new IdentityRole(DbUserRole.Admin));
                if (!await roleManager.RoleExistsAsync(DbUserRole.User))
                    await roleManager.CreateAsync(new IdentityRole(DbUserRole.User));

                if (await roleManager.RoleExistsAsync(DbUserRole.Admin))
                {
                    await userManager.AddToRoleAsync(user, DbUserRole.Admin);
                }
                if (await roleManager.RoleExistsAsync(DbUserRole.Admin))
                {
                    await userManager.AddToRoleAsync(user, DbUserRole.User);
                }
                return Ok("sign up success");
            }
            return BadRequest("User is already exists");
        }
        //PUT
        [HttpPut]
        public IActionResult Put()
        {
            return Ok("put method");
        }
        //DELETE
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("dedlete method");
        }
    }
}
