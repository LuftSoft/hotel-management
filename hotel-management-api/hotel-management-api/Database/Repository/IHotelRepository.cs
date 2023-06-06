using hotel_management_api.Database.Model;

namespace hotel_management_api.Database.Repository
{
    public interface IHotelRepository
    {
        Task<Hotel?> FindByIdAsync(int id);
        Task<Hotel> getOne(int id);
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task<Hotel> createAsync(Hotel hotel);
        Task<bool> updateAsync(Hotel hotel);
        Task<bool> deleteAsync(int id);
        Task<IEnumerable<int>> HotelNumPeopleAsync(int numOfPeople);
        Task<IEnumerable<int>> HotelPriceFindAsync(double price);
        Task<IEnumerable<int>> HotelFilterAsync(DateTime fromDate, int roomCount, int roomSize, double roomPrice);
    }
}
