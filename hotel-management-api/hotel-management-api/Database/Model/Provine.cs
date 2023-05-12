using System.ComponentModel.DataAnnotations;

namespace hotel_management_api.Database.Model
{
    public class Provine
    {
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public ICollection<District>? Districts { get; set; }
    }
}
