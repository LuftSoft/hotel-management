using System.ComponentModel.DataAnnotations;

namespace hotel_management_api.Database.Model
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public string? UserId { get; set; }    
        public string? Status { get; set; }
        public AppUser? User { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
