using hotel_management_api.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace hotel_management_api.Database.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext appDbContext;
        public BookingRepository(AppDbContext appDbContext) 
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Booking> GetOne(int id) 
        {
            return await appDbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<Booking>> GetByUserId(string id)
        {
            return await appDbContext.Bookings.Where(x => x.UserId == id).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetByHotelId(int id)
        {
            var rooms = await appDbContext.Rooms.Where(r => r.HotelId == id).Select(r => r.Id).ToListAsync();
            return await appDbContext.Bookings.Where(x => rooms.Contains(x.RoomId)).ToListAsync();
        }
        public async Task<IEnumerable<object>> BookingDay(DateTime fromDate)
        {
            return await appDbContext.Bookings.Where(b => b.IsReturned == false && b.ToDate < fromDate)
                .Select(b => new
                {
                    id = b.Id
                }).ToListAsync();
        }
        public async Task<bool> Create(Booking booking)
        {
            try
            {
                appDbContext.Bookings.Add(booking);
                int isSuccess = await appDbContext.SaveChangesAsync();
                return isSuccess > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Booking repository(Create). Error: {ex}");
                return false;
            }
        }
        public async Task<bool> Update(Booking booking)
        {
            try
            {
                var bookingInfor = await appDbContext.Bookings.FirstOrDefaultAsync(b => b.Id == booking.Id);
                if(bookingInfor == null)
                {
                    return false;
                }
                bookingInfor.UpdateDate = DateTime.Now;
                bookingInfor.FromDate = booking.FromDate;
                bookingInfor.ToDate = booking.ToDate;
                bookingInfor.Status = booking.Status;
                bookingInfor.Comments = booking.Comments;
                bookingInfor.IsReturned = booking.IsReturned;
                appDbContext.Bookings.Update(bookingInfor);
                int isSuccess = await appDbContext.SaveChangesAsync();
                return isSuccess > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Booking repository(Update). Error: {ex}");
                return false;
            }
        }
        public async Task<bool> Cancel(int id)
        {
            try
            {
                var booking = await appDbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id);
                if(booking == null)
                {
                    return false;
                }
                booking.IsReturned = false;
                appDbContext.Bookings.Update(booking);
                int isSuccess = await appDbContext.SaveChangesAsync();
                return isSuccess > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Booking repository(Cancel). Error: {ex}");
                return false;
            }
        }
    }
}
