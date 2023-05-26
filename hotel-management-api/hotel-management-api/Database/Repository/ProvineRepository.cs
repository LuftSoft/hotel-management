using hotel_management_api.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace hotel_management_api.Database.Repository
{
    public class ProvineRepository : IProvineRepository
    {
        private readonly AppDbContext appDbContext;
        public ProvineRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Provine>> GetListProovine()
        {
            return await appDbContext.Provines.ToListAsync();
        }
        public async Task<Provine?> FindByIdAsync(string? id)
        {
            return await appDbContext.Provines.FirstOrDefaultAsync(h => h.Id.Equals(id));
        }
        public async Task<IEnumerable<Provine>?> FindByNameAsync(string? key)
        {
            return await appDbContext.Provines.Where(h => h.Name.ToLower().Equals(key.ToLower())).ToListAsync();
        }
    }
}
