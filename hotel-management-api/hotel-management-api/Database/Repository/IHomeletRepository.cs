using hotel_management_api.Database.Model;
using System;

namespace hotel_management_api.Database.Repository
{
    public interface IHomeletRepository
    {
        Task<Homelet?> FindByIdAsync(string? id);
        Task<IEnumerable<Homelet>?> FindByNameAsync(string? key);
        Task<IEnumerable<Homelet>?> FindByDistrictIdAsync(string? id);
    }
}
