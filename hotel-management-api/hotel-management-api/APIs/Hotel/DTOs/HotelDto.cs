using hotel_management_api.Database.Model;

namespace hotel_management_api.APIs.Hotel.DTOs
{
    public class HotelDto
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
        public int HomeletId { set; get; }
    }
}
