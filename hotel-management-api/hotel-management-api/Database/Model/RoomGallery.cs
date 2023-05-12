namespace hotel_management_api.Database.Model
{
    public class RoomGallery
    {
        public int Id { get; set; }
        public int RooomId { get; set; }
        public Room? Room { get; set; }
        public string? Link { get; set; }
    }
}
