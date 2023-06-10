using hotel_management_api.APIs.Comment.DTOs;
using hotel_management_api.APIs.Location.DTOs;
using hotel_management_api.Business.Services;
using hotel_management_api.Database.Model;

namespace hotel_management_api.Business.Interactor.Location
{
    public interface IGetHomeletInteractor 
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public string DistrictId { get; set; }
            public List<HomeletDto> Data { get; set; }
        }
        Task<IGetHomeletInteractor.Response> GetAsync(string districtId);
    }
    public class GetHomeletInteractor: IGetHomeletInteractor
    {
        private readonly ILocationService locationService;
        public GetHomeletInteractor(ILocationService locationService) 
        {
            this.locationService = locationService;
        }
        public async Task<IGetHomeletInteractor.Response> GetAsync(string districtId)
        {
            return await locationService.GetHomelet(districtId);
        }
    }
}
