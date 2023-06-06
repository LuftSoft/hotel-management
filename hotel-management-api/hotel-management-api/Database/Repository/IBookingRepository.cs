using hotel_management_api.Database.Model;

namespace hotel_management_api.Database.Repository
{
    public interface IBookingRepository
    {
        Task<Booking> GetOne(int id);
        Task<IEnumerable<Booking>> GetByUserId(string id);
        Task<IEnumerable<Booking>> GetByHotelId(int id);
        Task<bool> Create(Booking booking);
        Task<bool> Update(Booking booking);
        Task<bool> Cancel(int id);
    }
}
