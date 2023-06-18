using hotel_management_api.Database.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace hotel_management_api.Database.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _appDbContext;
        public HotelRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Hotel> getOne(int id)
        {
            return await _appDbContext.Hotels.FirstOrDefaultAsync(h => h.Id == id);
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
        public async Task<Hotel?> FindByIdAsync(int id)
        {
            return await _appDbContext.Hotels
                .Include(h => h.HotelBenefit)
                .FirstOrDefaultAsync(h => h.Id == id);
        }
        public async Task<bool> updateAsync(Hotel hotel)
        {
            _appDbContext.Hotels.Update(hotel);
            int isSuccess = await _appDbContext.SaveChangesAsync();
            return isSuccess > 0;

        }
        public async Task<Hotel?> createAsync(Hotel hotel)
        {
            _appDbContext.Hotels.Add(hotel);
            int isSuccess = await _appDbContext.SaveChangesAsync();
            return hotel;
        }
        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return  await _appDbContext.Hotels.AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<int>> HotelPriceFindAsync(double price)
        {
            return await _appDbContext.Rooms.Where(r => r.Price <= price).Select(r => r.HotelId).Distinct().ToListAsync();
        }
        public async Task<IEnumerable<int>> HotelNumPeopleAsync(int numOfPeople)
        {
            return await _appDbContext.Rooms.Where(r => r.NumOfPeope > numOfPeople).Select(r => r.HotelId).Distinct().ToListAsync();
        }
        public async Task<IEnumerable<int>> HotelFilterAsync(DateTime fromDate, int roomCount, int roomSize)
        {
            var bookingRooms = await _appDbContext.Bookings.AsNoTracking().Where(b => b.ToDate >= fromDate && b.Returned == false)
                .GroupBy(b => b.RoomId)
                .Select(b => new
                {
                    id = b.Key,
                    count = b.Count()
                }).ToListAsync();
            var rooms = await _appDbContext.Rooms
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
