using System.ComponentModel.DataAnnotations;

namespace hotel_management_api.Database.Model
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Star { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? LogoLink { get; set; }
        public string? Slug { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
