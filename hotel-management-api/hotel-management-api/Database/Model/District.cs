using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_management_api.Database.Model
{
    [Table("District")]
    public class District
    {
        [Key]
        [Column(TypeName = "nvarchar(5)")]
        public string? Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string? Name { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Type { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public string? ProvineId { get; set; }
        public Provine? Provine { get; set; }
        public ICollection<Homelet>? Homelets { get; set; }
    }
}
