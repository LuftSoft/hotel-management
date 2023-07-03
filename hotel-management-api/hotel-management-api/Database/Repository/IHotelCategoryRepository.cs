using hotel_management_api.APIs.Categories.DTOs;
using hotel_management_api.Database.Model;

namespace hotel_management_api.Database.Repository
{
    public interface IHotelCategoryRepository
    {
        Task<bool> DeleteAsync(int id);
        Task<HotelCategory> GetById(int id);
        Task<IEnumerable<HotelCategory>> GetAll();
        Task<HotelCategory> GetByCategoryId(int cateId);
        Task<HotelCategory> CreateAsync(string cateName);
        Task<HotelCategory> UpdateAsync(CategoryDto dto);
        Task<HotelCategory> GetByCategoryName(string cateName);
    }
}
