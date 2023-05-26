using hotel_management_api.Database.Model;

namespace hotel_management_api.Database.Repository
{
    public interface IHotelRepository
    {
        Task<Hotel?> FindByIdAsync(int id);
        Task<Hotel> getOne(int id);
        Task<IEnumerable<Hotel>> findAllAsync();
        Task<Hotel> createAsync(Hotel hotel);
        Task<bool> updateAsync(Hotel hotel);
        Task<bool> deleteAsync(int id);
    }
}
