namespace hotel_management_api.Database.Model
{
    public class HotelBenefit
    {
        public int Id { get; set; }
        public bool Pool { get; set; }
        public bool FreeBreakfast { get; set; }
        public bool BBQParty { get; set; }
        public bool CarBorow { get; set; }
        public int HotelId { get; set; }
        public Hotel? hotel { get; set; }
    }
}
