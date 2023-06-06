using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_management_api.Database.Model
{
    [Table("Room")]
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfPeope { get; set; }
        public int NumOfBed { get; set; } 
        public string TypeOfBed { get; set; }      
        public string? Description { get; set; }
        public double Price { get; set; }
        //So luong phong nhu nay la bao nhieu
        public double? Square { get; set; }
        public int TotalRoom { get; set; }  
        public bool Refund { get; set; }
        public bool Reschedule { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public ICollection<Booking>? Bookings { get; set; }  
        public ICollection<RoomGallery>? RoomGalleries { get; set; } 
    }
}
