using hotel_management_api.APIs.Room.DTOs;
using hotel_management_api.Database.Model;

namespace hotel_management_api.Database.Repository
{
    public interface IRoomRepository
    {
        Task<string?> GetUserIdAsync(int id);
        Task<Room?> GetByIdAsync(int id);
        Task<IEnumerable<RoomDetailDto>> GetAllAsync();
        Task<IEnumerable<RoomDetailDto>> GetByHotelIdAsync(int hotelId);
        Task<Room?> CreateAsync(Room room);
        Task<bool> UpdateAsync(Room room);
        Task<bool> DeleteAsync(int id);

    }
}
