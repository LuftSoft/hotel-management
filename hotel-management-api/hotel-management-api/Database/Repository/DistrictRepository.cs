using hotel_management_api.Database.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace hotel_management_api.Database.Repository
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly AppDbContext appDbContext;
        public DistrictRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<District?> FindByIdAsync(string? id)
        {
            return await appDbContext.Districts.FirstOrDefaultAsync(h => h.Id.Equals(id));
        }
        public async Task<IEnumerable<District>?> FindByNameAsync(string? key)
        {
            return await appDbContext.Districts.AsNoTracking().Where(h => h.Name.ToLower().Equals(key.ToLower())).ToListAsync();
        }
        public async Task<IEnumerable<District>?> FindByProvineIdAsync(string? id)
        {
            return await appDbContext.Districts.AsNoTracking().Where(h => h.ProvineId.Equals(id)).ToListAsync();
        }
    }
}
