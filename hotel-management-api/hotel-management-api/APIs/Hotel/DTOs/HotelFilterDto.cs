using System.ComponentModel;

namespace hotel_management_api.APIs.Hotel.DTOs
{
    public class HotelFilterDto
    {   
        //1:: Cao -> Thap, 2: Thap -> cao
        [DefaultValue(null)]
        public int? PriceSort { get; set; }
        [DefaultValue(null)]
        public double? MinPrice { get; set; }
        [DefaultValue(null)]
        public double? MaxPrice { get; set; }
        [DefaultValue(null)]
        public bool? Resttaurant { get; set; }
        [DefaultValue(null)]
        public bool? AllTimeFrontDesk { get; set; }
        [DefaultValue(null)]
        public bool? Elevator { get; set; }
        [DefaultValue(null)]
        public bool? Pool { get; set; }
        [DefaultValue(null)]
        public bool? FreeBreakfast { get; set; }
        [DefaultValue(null)]
        public bool? AirConditioner { get; set; }
        [DefaultValue(null)]
        public bool? CarBorow { get; set; }
        [DefaultValue(null)]
        public bool? WifiFree { get; set; }
        [DefaultValue(null)]
        public bool? Parking { get; set; }
        [DefaultValue(null)]
        public bool? AllowPet { get; set; }
    }
}
