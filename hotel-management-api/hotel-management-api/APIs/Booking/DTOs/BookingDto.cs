using hotel_management_api.Database.Model;

namespace hotel_management_api.APIs.Booking.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string? Status { get; set; }
        public bool? IsReturned { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
