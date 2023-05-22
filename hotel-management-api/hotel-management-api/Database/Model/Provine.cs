using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_management_api.Database.Model
{
    [Table("Provine")]
    public class Provine
    {
        [Key]
        public int? Id { get; set; }
        public string? ProvineCode { get; set; }
        public string? Name { get; set; }
        public ICollection<District>? Districts { get; set; }
    }
}
