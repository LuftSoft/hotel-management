using hotel_management_api.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace hotel_management_api.Database.Repository
{
    public class RoomGalleryRepository : IRoomGalleryRepository
    {
        private readonly AppDbContext appDbContext;
        public RoomGalleryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<RoomGallery> GetByIdAsync(int id)
        {
            return await appDbContext.RoomGalleries.Where(r => r.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<RoomGallery>> GetByRoomIdAsync(int id)
        {
            return await appDbContext.RoomGalleries.Where(r => r.RooomId == id).ToListAsync();
        }
        public async Task<IEnumerable<RoomGallery>> GetByHotelIdAsync(int id)
        {
            try
            {
                List<Room> rooms = await appDbContext.Rooms.Where(r => r.HotelId == id).ToListAsync();
                return await appDbContext.RoomGalleries.Join(
                    rooms,
                    rg => rg.RooomId,
                    r => r.Id,
                    (rg, r) => rg
                    ).ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error-RoomGalleryRepository-GetByHotelIdAsync->{ex.Message}");
                return null;
            }
        }
        public async Task<RoomGallery?> CreateAsync(RoomGallery roomGallery)
        {
            try
            {
                appDbContext.RoomGalleries.Add(roomGallery);
                await appDbContext.SaveChangesAsync();
                return roomGallery;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error-RoomGalleryRepository-CreateAsync->{ex.Message}");
                return null;
            }
        }
        public async Task<bool> UpdateAsync(RoomGallery roomGallery)
        {
            try
            {
                appDbContext.RoomGalleries.Update(roomGallery);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error-RoomGalleryRepository-UpdateAsync->{ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var rommGallery = await appDbContext.RoomGalleries.FirstOrDefaultAsync(r => r.Id == id);
                if (rommGallery == null)
                {
                    return false;
                }
                appDbContext.RoomGalleries.Remove(rommGallery);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch( Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error-RoomGalleryRepository-DeleteAsync->{ex.Message}");
                return false;
            }

        }
    }
}
