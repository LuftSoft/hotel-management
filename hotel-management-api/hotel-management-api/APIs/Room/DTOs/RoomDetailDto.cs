using hotel_management_api.Database.Repository;

namespace hotel_management_api.APIs.Room.DTOs
{
    public class RoomDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfPeope { get; set; }
        public int NumOfBed { get; set; }
        public string TypeOfBed { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double? Square { get; set; }
        //So luong phong nhu nay la bao nhieu
        public int TotalRoom { get; set; }
        //hoa`n tie`n   
        public bool Refund { get; set; }
        //do?i li.ch
        public bool Reschedule { get; set; }
        public int HotelId { get; set; }
        public List<RoomGalleryDto> HotelImageGalleries { get; set; }
    }
}
