using System.ComponentModel.DataAnnotations;

namespace hotel_management_api.Database.Model
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Size { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
