using hotel_management_api.Database.Model;
using System.ComponentModel;

namespace hotel_management_api.APIs.Hotel.DTOs
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Star { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? LogoLink { get; set; }
        public string? Slug { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UserId { get; set; }
        public bool? Approval { get; set; }
        public HotelCategory? HotelCategory { set; get; }
        public int HotelCategoryId { set; get; }
        public HotelBenefit? HotelBenefit { set; get; }
        public string HomeletId { set; get; }
        [DefaultValue(0)]
        public double? MinPrice { set; get; }
        [DefaultValue(0)]
        public double? MaxPrice { set; get; }
    }
}
