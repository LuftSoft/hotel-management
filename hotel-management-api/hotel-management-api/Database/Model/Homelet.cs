using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_management_api.Database.Model
{
    [Table("Homelet")]
    public class Homelet
    {
        [Key]
        [Column(TypeName = "nvarchar(5)")]
        public string? Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string? Name { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Type { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public string? DistrictId { get; set; }
        public District? District { get; set; }
        public ICollection<Hotel>? Hotels { get; set; }
    }
}
