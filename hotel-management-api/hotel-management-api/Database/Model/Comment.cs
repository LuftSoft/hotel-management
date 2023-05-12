using System.ComponentModel.DataAnnotations;

namespace hotel_management_api.Database.Model
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime? LastChange { get; set; }
        public int BookingId { get; set; }
        public Booking? Booking { get; set; }
    }
}
