using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_management_api.Database.Model
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public double Rating { get; set; }
        public string? Content { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastChange { get; set; }
        public int BookingId { get; set; }
        public Booking? Booking { get; set; }
    }
}
