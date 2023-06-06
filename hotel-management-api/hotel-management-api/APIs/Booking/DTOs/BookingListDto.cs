namespace hotel_management_api.APIs.Booking.DTOs
{
    public class BookingListDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int RoomSize { get; set; }
        public string RoomName { get; set; }
        public string? Status { get; set; }
        public bool? IsReturned { get; set; }
        public int? HotelId { get; set; }
        public string? HotelName { get; set; }
        public string? HotelImage { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
