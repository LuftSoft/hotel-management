namespace hotel_management_api.APIs.User.UserDTOs
{
    public class UpdateUserDto
    {
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? LastName { get; set;}
        public string? FirstName { get; set; }
        public IFormFile? Avatar { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
