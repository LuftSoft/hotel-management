using hotel_management_api.APIs.Room.DTOs;
using hotel_management_api.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace hotel_management_api.Database.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext appDbContext;
        public RoomRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        private RoomDetailDto ConvertToRoomDetailDto(Room room, List<RoomGalleryDto> roomGalleryDtos)
        {
            return new RoomDetailDto()
            {
                Id = room.Id,
                Name = room.Name,
                NumOfPeope = room.NumOfPeope,
                NumOfBed = room.NumOfBed,
                TypeOfBed = room.TypeOfBed,
                Description = room.Description,
                Price = room.Price,
                Square = room.Square,
                //So luong phong nhu nay la bao nhieu
                TotalRoom = room.TotalRoom,
                //hoa`n tie`n   
                Refund = room.Refund,
                //do?i li.ch
                Reschedule = room.Reschedule,
                HotelId = room.HotelId,
                HotelImageGalleries = roomGalleryDtos
            };
        }
        //get owner of room
        public async Task<string?> GetUserIdAsync(int id)
        {
            return await appDbContext.Rooms.AsNoTracking().Where(r => r.Id == id)
                .Include(r => r.Hotel)
                .Select(r => r.Hotel.USerId).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<int>> HotelRoomSizeAsync(double price)
        {
            var x = await appDbContext.Bookings.Where(b => b.Returned == false && b.ToDate> DateTime.Now).Select(b => b.RoomId).ToListAsync();
            return await appDbContext.Rooms.Where(r => r.Price > price).Select(r => r.HotelId).Distinct().ToListAsync();
        }
        public async Task<Room> GetByIdAsync(int id)
        {
            return await appDbContext.Rooms.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<IEnumerable<RoomDetailDto>?> GetAllAsync()
        {
            return null;
            //return await appDbContext.Rooms.ToListAsync();
        }
        public async Task<IEnumerable<RoomDetailDto>?> GetByHotelIdAsync(int hotelId)
        {
            List<RoomDetailDto> results = new List<RoomDetailDto> ();
            var rooms = await appDbContext.Rooms.AsNoTracking().Where(r => r.HotelId == hotelId).ToListAsync();
            foreach(var r in rooms) 
            {
                List<RoomGalleryDto> dtos = await appDbContext.RoomGalleries.Where(rg => rg.RooomId == r.Id)
                    .Select(r => new RoomGalleryDto()
                    {
                        Id = r.Id,
                        RooomId = r.RooomId,
                        Link = r.Link
                    }).AsNoTracking().ToListAsync();
                results.Add(ConvertToRoomDetailDto(r,dtos));
            }
            return results;
            //return await appDbContext.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();
        }
        public async Task<Room?> CreateAsync(Room room)
        {
            try
            {
                appDbContext.Rooms.Add(room);
                await appDbContext.SaveChangesAsync();
                return room;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error-RoomRepository-CreateAsync->{ex.Message}");
                return null;
            }
        }
        public async Task<bool> UpdateAsync(Room room)
        {
            try
            {
                appDbContext.Rooms.Update(room);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Roomrepository->update: {ex}");
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Room? room = await appDbContext.Rooms.FirstOrDefaultAsync(r => r.Id == id);
                if (room == null)
                {
                    return false;
                }
                appDbContext.Rooms.Remove(room);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Roomrepository->update: {ex}");
                return false;
            }
        }
    }
}
