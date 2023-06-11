using hotel_management_api.Database.Model;

namespace hotel_management_api.Database.Repository
{
    public interface IHotelBenefitRepository
    {
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<HotelBenefit>> GetAll();
        Task<HotelBenefit?> FindByIdAsync(int id);
        Task<bool> UpdateAsync(HotelBenefit benefit);
        Task<HotelBenefit?> FindByHotelIdAsync(int id);
        Task<int> CreateAsync(HotelBenefit hotelBenefit);
        Task<IEnumerable<HotelBenefit>> GetPaging(int pageIndex, int pageSize);
    }
}
