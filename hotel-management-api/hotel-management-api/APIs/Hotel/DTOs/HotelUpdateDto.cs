namespace hotel_management_api.APIs.Hotel.DTOs
{
    public class HotelUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Star { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public IFormFile? Logo { get; set; }
        public string? Slug { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? USerId { get; set; }
        public int HotelCategoryId { set; get; }
        public int HotelBenefitId { set; get; }
        public string? HomeletId { set; get; }
        public bool? Resttaurant { get; set; }
        public bool? AllTimeFrontDesk { get; set; }
        public bool? Elevator { get; set; }
        public bool Pool { get; set; }
        public bool FreeBreakfast { get; set; }
        public bool AirConditioner { get; set; }
        public bool CarBorow { get; set; }
        public bool WifiFree { get; set; }
        public bool Parking { get; set; }
        public bool AllowPet { get; set; }
    }
}
