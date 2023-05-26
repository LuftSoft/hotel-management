using hotel_management_api.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace hotel_management_api.Database.Repository
{
    public class HomeletRepository : IHomeletRepository
    {
        private readonly AppDbContext appDbContext;
        public HomeletRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Homelet?> FindByIdAsync(string? id)
        {
            return await appDbContext.Homelets.FirstOrDefaultAsync(h => h.Id.Equals(id));
        }
        public async Task<IEnumerable<Homelet>?> FindByNameAsync(string? key)
        {
            return await appDbContext.Homelets.Where(h => h.Name.ToLower().Equals(key.ToLower())).ToListAsync();
        }
        public async Task<IEnumerable<Homelet>?> FindByDistrictIdAsync(string? id)
        {
            return await appDbContext.Homelets.Where(h => h.DistrictId.Equals(id)).ToListAsync();
        }
    }
}
