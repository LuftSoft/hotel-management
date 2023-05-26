using hotel_management_api.Database.Model;

namespace hotel_management_api.Database.Repository
{
    public interface IDistrictRepository
    {
        Task<District?> FindByIdAsync(string? id);
        Task<IEnumerable<District>?> FindByNameAsync(string? key);
        Task<IEnumerable<District>?> FindByProvineIdAsync(string? id);
    }
}
