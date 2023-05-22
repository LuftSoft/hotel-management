using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_management_api.Database.Model
{
    [Table("RoomGallery")]
    public class RoomGallery
    {
        [Key]
        public int Id { get; set; }
        public int RooomId { get; set; }
        public Room? Room { get; set; }
        public string? Link { get; set; }
    }
}
