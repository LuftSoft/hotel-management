using hotel_management_api.Database.Model;

namespace hotel_management_api.Database.Repository
{
    public interface IHotelCategoryRepository
    {
        Task<HotelCategory> GetById(int id);
        Task<IEnumerable<HotelCategory>> GetAll();
    }
}
