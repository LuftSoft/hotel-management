using hotel_management_api.Database.Model;

namespace hotel_management_api.Database.Repository
{
    public class RoomGalleryDto
    {
        public int Id { get; set; }
        public int RooomId { get; set; }
        public string Link { get; set; }
    }
    public interface IRoomGalleryRepository
    {
        Task<RoomGallery> GetByIdAsync(int id);
        Task<IEnumerable<RoomGallery>> GetByRoomIdAsync(int id);
        Task<IEnumerable<RoomGallery>> GetByHotelIdAsync(int id);
        Task<RoomGallery?> CreateAsync(RoomGallery roomGallery);
        Task<bool> UpdateAsync(RoomGallery roomGallery);
        Task<bool> DeleteAsync(int id);
    }
}
