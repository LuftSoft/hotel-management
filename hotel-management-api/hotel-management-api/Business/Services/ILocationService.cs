using hotel_management_api.Business.Interactor.Location;

namespace hotel_management_api.Business.Services
{
    public interface ILocationService
    {
        Task<IGetHomeletInteractor.Response> GetHomelet(string districtId);
        Task<IGetDistrictInteractor.Response> GetDistrict(string provineId);
        Task<IGetProvineInteractor.Response> GetProvine();
    }
}
