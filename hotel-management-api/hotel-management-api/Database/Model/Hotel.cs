using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_management_api.Database.Model
{
    [Table("Hotel")]
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Star { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? GoogleLocation { get; set; }
        public string? LogoLink { get; set; }
        public string? Slug { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? USerId { get; set; }
        public AppUser? User { get; set; }
        public int HotelCategoryId { set; get; }
        public HotelCategory? HotelCategory { set; get; } 
        public int? HotelBenefitId { set; get; }
        public HotelBenefit? HotelBenefit { set; get; }
        public string? HomeletId { set; get; }
        public Homelet? Homelet { set; get; }
        public ICollection<Room>? Rooms { get; set; }
    }
}
