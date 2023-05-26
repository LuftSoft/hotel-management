using hotel_management_api.Database.Model;

namespace hotel_management_api.Database.Repository
{
    public interface IHotelBenefitRepository
    {
        Task<IEnumerable<HotelBenefit>> GetAll();
        Task<IEnumerable<HotelBenefit>> GetPaging(int pageIndex, int pageSize);
        Task<HotelBenefit?> FindByIdAsync(int id);
        Task<int> CreateAsync(HotelBenefit hotelBenefit);
        Task<bool> UpdateAsync(HotelBenefit benefit);
        Task<bool> DeleteAsync(int id);
    }
}
