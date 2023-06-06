namespace hotel_management_api.APIs.Hotel.DTOs
{
    public class GetListHotelFilterDto
    {
        public string? ProvineId { get; set; }
        public string? DistrictId { get; set; }
        public string? HomeletId { get; set; }
        public double Price { get; set; } = 0;
        public int RoonCount { get; set; } = 1;
        public int RoomSize { get; set; } = 1;
        public DateTime FromDate { get; set; } = DateTime.Now;
        public DateTime ToDate { get; set; } = DateTime.Now.AddDays(1);
    }
}
