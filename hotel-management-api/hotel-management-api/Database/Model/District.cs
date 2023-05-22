using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_management_api.Database.Model
{
    [Table("District")]
    public class District
    {
        [Key]
        public int? Id { get; set; }
        public string? DistrictCode { get; set; }
        public string? Name { get; set; }
        public int? ProvineId { get; set; }
        public Provine? Provine { get; set; }
        public ICollection<Homelet>? Homelets { get; set; }
    }
}
