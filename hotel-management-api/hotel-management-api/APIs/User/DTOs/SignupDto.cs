using System.ComponentModel;

namespace hotel_management_api.APIs.User.UserDTOs
{
    public class SignupDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public List<string>? Role { get; set; }
        public SignupDto(string? FirstName,string? LastName, string? userName, string? password, List<string>? role)
        {
            UserName = userName;
            Password = password;
            this.FirstName = FirstName;
            this.LastName = LastName;
            Role = role;
        }
    }
}
