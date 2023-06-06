namespace hotel_management_api.APIs.Room.DTOs
{
    public class CreateRoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfPeope { get; set; }
        public int NumOfBed { get; set; }
        public string TypeOfBed { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        //So luong phong nhu nay la bao nhieu
        public int TotalRoom { get; set; }
        public double Square { get; set; }
        public bool Refund { get; set; }
        public bool Reschedule { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int HotelId { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
