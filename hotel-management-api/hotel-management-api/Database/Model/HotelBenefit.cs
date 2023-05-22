using System.ComponentModel.DataAnnotations.Schema;
using static Humanizer.In;

namespace hotel_management_api.Database.Model
{
    [Table("HotelBenefit")]
    public class HotelBenefit
    {
        public int Id { get; set; }
        public bool? Resttaurant { get; set; }
        public bool? AllTimeFrontDesk { get; set; }
        public bool? Elevator { get; set; }
        public bool Pool { get; set; }
        public bool FreeBreakfast { get; set; }
        public bool BBQParty { get; set; }
        public bool CarBorow { get; set; }
        public bool WifiFree { get; set; }  
        public ICollection<Hotel> Hotels { get; set; }
    }
}
