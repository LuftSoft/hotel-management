namespace hotel_management_api.APIs.Room.DTOs
{
    public class CreateRoomGalleryDto
    {
        public int RoomId { get; set; }
        public IFormFile[] Images { get; set; }
    }
}
