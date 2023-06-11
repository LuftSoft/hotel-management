using hotel_management_api.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace hotel_management_api.Database.Repository
{
    public class HotelCategoryRepository:IHotelCategoryRepository
    {
        private readonly AppDbContext appDbContext;
        public HotelCategoryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IEnumerable<HotelCategory>> GetAll()
        {
            return await appDbContext.HotelCategories.AsNoTracking().ToListAsync();
        }
        public async Task<HotelCategory> GetById(int id)
        {
            return await appDbContext.HotelCategories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
