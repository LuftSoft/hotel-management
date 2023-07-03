using hotel_management_api.Database.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace hotel_management_api.Database.Repository
{
    public class HotelRepository :  IHotelRepository
    {
        private readonly AppDbContext appDbContext;
        public HotelRepository(AppDbContext _appDbContext)
        {
            this.appDbContext = _appDbContext;
        }
        public async Task<Hotel> getOne(int id)
        {
            return await appDbContext.Hotels.FirstOrDefaultAsync(h => h.Id == id);
        }
        public async Task<bool> Delete(int id)
        {
            var hotel = appDbContext.Hotels.FirstOrDefault(h => h.Id == id);
            if(hotel == null)
            {
                return false;
            }
            appDbContext.Remove(hotel);
            int isSuccess = await appDbContext.SaveChangesAsync();
            return isSuccess > 0;
        }
        public async Task<IEnumerable<Hotel>> GetByOwnerId(string userid)
        {
            return await appDbContext.Hotels.Where(h => h.USerId.Equals(userid)).ToListAsync();
        }
        public async Task<Hotel?> FindByIdAsync(int id)
        {
            return await appDbContext.Hotels
                .FirstOrDefaultAsync(h => h.Id == id);
        }
        public async Task<bool> updateAsync(Hotel hotel)
        {
            appDbContext.Hotels.Update(hotel);
            int isSuccess = await appDbContext.SaveChangesAsync();
            return isSuccess > 0;

        }
        public async Task<Hotel?> createAsync(Hotel hotel)
        {
            appDbContext.Hotels.Add(hotel);
            int isSuccess = await appDbContext.SaveChangesAsync();
            return hotel;
        }
        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return  await appDbContext.Hotels.AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<int>> HotelPriceFindAsync(double price)
        {
            return await appDbContext.Rooms.Where(r => r.Price <= price).Select(r => r.HotelId).Distinct().ToListAsync();
        }
        public async Task<IEnumerable<int>> HotelNumPeopleAsync(int numOfPeople)
        {
            return await appDbContext.Rooms.Where(r => r.NumOfPeope > numOfPeople).Select(r => r.HotelId).Distinct().ToListAsync();
        }
        public async Task<IEnumerable<int>> HotelFilterAsync(DateTime fromDate, int roomCount, int roomSize)
        {
            var bookingRooms = await appDbContext.Bookings.AsNoTracking().Where(b => b.ToDate >= fromDate && b.Returned == false)
                .GroupBy(b => b.RoomId)
                .Select(b => new
                {
                    id = b.Key,
                    count = b.Count()
                }).ToListAsync();
            var rooms = await appDbContext.Rooms
                .Select(r => new IHotelRepository.HotelFilterList()
                {
                    Id = r.Id,
                    RoomSize = r.NumOfPeope,
                    Price = r.Price,
                    TotalRoom = r.TotalRoom,
                    HotelId = r.HotelId
                }).ToListAsync();
            if (bookingRooms.Any())
            {
                foreach(var item in bookingRooms)
                {
                    for(int i=0;i<rooms.Count; i++)
                    {
                        if(rooms.ElementAt(i).Id == item.id)
                        {
                            rooms.ElementAt(i).TotalRoom = rooms.ElementAt(i).TotalRoom - item.count;
                        }
                    }
                }
                //rooms = rooms.Join(
                //bookingRooms,
                //r => r.Id,
                //b => b.id,
                //(r, b) =>
                //new IHotelRepository.HotelFilterList()
                //{
                //    Id = r.Id,
                //    RoomSize = r.RoomSize,
                //    Price = r.Price,
                //    TotalRoom = r.TotalRoom - b.count,
                //    HotelId = r.HotelId
                //}
                //).ToList();
            }
            var hotels = rooms.Where(r => r.RoomSize >= roomSize && r.TotalRoom >= roomCount)
                .Select(r => r.HotelId).Distinct().ToList();
            return hotels;
        }
    }
}
