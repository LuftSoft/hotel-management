using hotel_management_api.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace hotel_management_api.Database.Repository
{
    public interface IHotelRepository
    {
        Task<Hotel> getOne(int id);
        Task<IEnumerable<Hotel>> findAllAsync();
        Task<bool> createAsync(Hotel hotel);
        Task<bool> updateAsync(Hotel hotel);
        Task<bool> deleteAsync(int id);
    }
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _appDbContext;
        public HotelRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> createAsync(Hotel hotel)
        {
            _appDbContext.Hotels.Add(hotel);
            int isSuccess = await _appDbContext.SaveChangesAsync();
            return isSuccess > 0;
        }

        public async Task<bool> deleteAsync(int id)
        {
            var hotel = _appDbContext.Hotels.FirstOrDefault(h => h.Id == id);
            if(hotel == null)
            {
                return false;
            }
            _appDbContext.Remove(hotel);
            int isSuccess = await _appDbContext.SaveChangesAsync();
            return isSuccess > 0;
        }

        public async Task<IEnumerable<Hotel>> findAllAsync()
        {
            return await _appDbContext.Hotels.ToListAsync();
        }

        public async Task<Hotel> getOne(int id)
        {
            return await _appDbContext.Hotels.FirstOrDefaultAsync(h =>h.Id == id);
        }

        public async Task<bool> updateAsync(Hotel hotel)
        {
            _appDbContext.Hotels.Update(hotel);
            int isSuccess = await _appDbContext.SaveChangesAsync();
            return isSuccess > 0;

        }
    }
}
