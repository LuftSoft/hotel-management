using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hotel_management_api.Utils
{   
    public interface IJwtUtil
    {
        JwtSecurityToken GenerateAccessToken(List<Claim> authClaims, string u);
        JwtSecurityToken GenerateRefreshToken(List<Claim> authClaims, string u);
        string? getUserNameFromToken(string token);
        bool isTokenExpired(string token);
        JwtSecurityToken GenerateResetPasswordApiToken(string username);
        string? getTokenFromHeader(HttpContext context);
    }
    public class JwtUtil : IJwtUtil
    {   
        private readonly IConfiguration configuration;
        public JwtUtil(IConfiguration _configuration) 
        {
            this.configuration = _configuration;
        }
        public JwtSecurityToken GenerateAccessToken(List<Claim> authClaims, string username)
        {
            authClaims.Add(new Claim(type: "AccessToken", "true"));
            authClaims.Add(new Claim(type: "UserName", value: username));
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfig:AccessSecret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWTConfig:ValidIssuer"],
                audience: configuration["JWTConfig:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        public JwtSecurityToken GenerateRefreshToken(List<Claim> authClaims, string username)
        {
            authClaims.Add(new Claim(type:"RefreshToken", "true"));
            authClaims.Add(new Claim(type:"UserName", value: username));
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfig:RefreshSecret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWTConfig:ValidIssuer"],
                audience: configuration["JWTConfig:ValidAudience"],
                expires: DateTime.Now.AddDays(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        public JwtSecurityToken GenerateResetPasswordApiToken(string username)
        {   
            List<Claim> authClaims = new List<Claim>();
            authClaims.Add(new Claim(type: "ResetPassword", "true"));
            authClaims.Add(new Claim(type: "UserName", value: username));
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfig:RefreshPasswordSecret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWTConfig:ValidIssuer"],
                audience: configuration["JWTConfig:ValidAudience"],
                expires: DateTime.Now.AddMinutes(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        public string? getUserNameFromToken(string token) 
        {   
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            if (jwt == null)
                return "jwt is null";
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == "UserName");
            if (userId == null) 
                return "username is null";
            return userId.Value;
        }
        public bool isTokenExpired(string? token)
        {   
            if(token == null) return true;
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            if(jwt == null) 
                return true;
            var expired = jwt.ValidTo;
            if(expired > DateTime.Now) 
                return false;
            return true;

        }
        public string? getTokenFromHeader(HttpContext context)
        {
            var listAuth = context.Request.Headers.Authorization.ToList();
            foreach (var item in listAuth)
            {
                if (item.Split(' ')[0].Equals("Bearer")) 
                    return item.Split(" ")[1];
            }
            return null;
        }
    }
}
