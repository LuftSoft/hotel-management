using hotel_management_api.Database.Repository;

namespace hotel_management_api.Business.Services
{   
    public interface IHotelService
    {

    }
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository hotelRepository;
        public HotelService(IHotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }
    }
}
