using System.ComponentModel.DataAnnotations;

namespace hotel_management_api.Database.Model
{
    public class District
    {
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? ProvineId { get; set; }
        public Provine? Provine { get; set; }
        public ICollection<Homelet>? Homelets { get; set; }
    }
}
