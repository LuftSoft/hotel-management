namespace hotel_management_api.APIs.User.UserDTOs
{
    public class UpdateUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
        public string? Email { get; set; }
        public int? Age { get; set; }
        public string? Avatar { get; set; }
    }
}
