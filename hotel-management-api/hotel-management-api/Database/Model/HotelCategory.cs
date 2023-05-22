using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_management_api.Database.Model
{
    [Table("HotelCategory")]
    public class HotelCategory
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Hotel>? Hotels { set; get; } 
    }
}
