using hotel_management_api.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace hotel_management_api.Database.Repository
{   
    public class HotelBenefitRepository : IHotelBenefitRepository
    {
        private readonly AppDbContext appDbContext;
        public HotelBenefitRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IEnumerable<HotelBenefit>> GetAll() 
        {
            return await appDbContext.HotelBenefits.ToListAsync();
        }
        public async Task<IEnumerable<HotelBenefit>> GetPaging(int pageIndex, int pageSize)
        {
            return await appDbContext.HotelBenefits.Skip(pageIndex*pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<HotelBenefit?> FindByIdAsync(int id)
        {
            return await appDbContext.HotelBenefits.FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<HotelBenefit?> FindByHotelIdAsync(int id)
        {
            int? Benefitid = await appDbContext.Hotels.Where(h => h.Id == id).Select(h => h.HotelBenefitId).FirstOrDefaultAsync();
            return await appDbContext.HotelBenefits.FirstOrDefaultAsync(b => b.Id == Benefitid);
        }
        public async Task<int> CreateAsync(HotelBenefit hotelBenefit)
        {
            try
            {
                if (hotelBenefit == null)
                {
                    return 0;
                }
                appDbContext.HotelBenefits.Add(hotelBenefit);
                await appDbContext.SaveChangesAsync();
                return hotelBenefit.Id;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"HotelBenefitRepository -> CreateAsync - {ex}");
                return 0;
            }
        }
        public async Task<bool> UpdateAsync(HotelBenefit benefit)
        {
            try
            {
                var bnfit = await appDbContext.HotelBenefits.FirstOrDefaultAsync(h => h.Id == benefit.Id);
                if (bnfit == null)
                    return false;
                appDbContext.HotelBenefits.Update(bnfit);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"HotelBenefitRepository -> UpdateAsync - {ex}");
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var hbenefit = await appDbContext.HotelBenefits.FirstOrDefaultAsync(h => h.Id == id);
                if (hbenefit == null)
                    return false;
                appDbContext.HotelBenefits.Remove(hbenefit);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"HotelBenefitRepository -> UpdateAsync - {ex}");
                return false;
            }
        }
    }
}
