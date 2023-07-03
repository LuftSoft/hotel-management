using hotel_management_api.APIs.Categories.DTOs;
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
        public async Task<HotelCategory> GetByCategoryName(string cateName)
        {
            try
            {
                HotelCategory cate = appDbContext.HotelCategories.FirstOrDefault(c => c.Name.ToLower() == cateName.ToLower());
                return cate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"category repos(create): {ex.Message}");
                return null;
            }
        }
        public async Task<HotelCategory> GetByCategoryId(int cateId)
        {
            try
            {
                HotelCategory cate = appDbContext.HotelCategories.FirstOrDefault(c => c.Id == cateId);
                return cate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"category repos(create): {ex.Message}");
                return null;
            }
        }
        public async Task<HotelCategory> CreateAsync(string cateName)
        {
            try
            {
                HotelCategory cate = new HotelCategory { Id = 0, Name = cateName };
                appDbContext.HotelCategories.Add(cate);
                await appDbContext.SaveChangesAsync();
                return cate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"category repos(create): {ex.Message}");
                return null;
            }
        }
        public async Task<HotelCategory> UpdateAsync(CategoryDto dto)
        {
            HotelCategory updateCate = await appDbContext.HotelCategories.Where(c => c.Id == dto.Id).FirstOrDefaultAsync();
            updateCate.Name = dto.Name;
            appDbContext.HotelCategories.Update(updateCate);
            await appDbContext.SaveChangesAsync();
            return updateCate;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                HotelCategory delCate = await appDbContext.HotelCategories.Where(c => c.Id == id).FirstOrDefaultAsync();
                appDbContext.HotelCategories.Remove(delCate);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"category repos: {ex.Message}");
                return false;
            }
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
