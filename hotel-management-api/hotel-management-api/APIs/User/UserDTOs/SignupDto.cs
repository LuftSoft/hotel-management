namespace hotel_management_api.APIs.User.UserDTOs
{
    public class SignupDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public SignupDto(string? userName, string? password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
