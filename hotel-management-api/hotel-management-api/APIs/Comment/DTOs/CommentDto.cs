namespace hotel_management_api.APIs.Comment.DTOs
{
    public class CommentDto
    {
        public int? Id { get; set; }
        public double Rating { get; set; }
        public string Content { get; set; }
        public string? UserName { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastChange { get; set; }
        public int BookingId { get; set; }
    }
}
