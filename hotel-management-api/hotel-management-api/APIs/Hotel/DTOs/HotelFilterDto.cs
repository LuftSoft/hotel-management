namespace hotel_management_api.APIs.Hotel.DTOs
{
    public class HotelFilterDto
    {   
        //1:: Cao -> Thap, 2: Thap -> cao
        public int? PriceSort { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public bool? Resttaurant { get; set; }
        public bool? AllTimeFrontDesk { get; set; }
        public bool? Elevator { get; set; }
        public bool? Pool { get; set; }
        public bool? FreeBreakfast { get; set; }
        public bool? AirConditioner { get; set; }
        public bool? CarBorow { get; set; }
        public bool? WifiFree { get; set; }
        public bool? Parking { get; set; }
        public bool? AllowPet { get; set; }
    }
}
