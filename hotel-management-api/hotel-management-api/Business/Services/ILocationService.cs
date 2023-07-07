using hotel_management_api.Business.Interactor.Location;

namespace hotel_management_api.Business.Services
{
    public interface ILocationService
    {
        Task<IGetProvineInteractor.Response> GetProvine();
        Task<IGetHomeletInteractor.Response> GetHomelet(string districtId);
        Task<IGetDistrictInteractor.Response> GetDistrict(string provineId);
        Task<IGetHomeletInteractor.DetailHomeletResponse> GetDetailHomeletAsync(string homeletId);
        Task<IGetProvineInteractor.DetailProvineResponse> GetDetailProvineAsync(string provineId);
        Task<IGetDistrictInteractor.DetailDistrictResponse> GetDetailDistrictAsync(string district);
    }
}
