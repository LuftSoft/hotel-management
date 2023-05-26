namespace hotel_management_api.APIs.Hotel.DTOs
{
    public class GetListHotelFilterDto
    {
        public string? ProvineId { get; set; }
        public string? DítrictId { get; set; }
        public string? HomeletId { get; set; }
        public int? RoomSize { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set;}
    }
}
