using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_management_api.Database.Model
{
    [Table("Homelet")]
    public class Homelet
    {
        [Key]
        public int? Id { get; set; }
        public string? HomeletCode { get; set; }
        public string? Name { get; set; }
        public int? DistrictId { get; set; }
        public District? District { get; set; }
        public ICollection<Hotel>? Hotels { get; set; }
    }
}
