using hotel_management_api.APIs.Comment.DTOs;
using hotel_management_api.APIs.Location.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Location
{
    public interface IGetProvineInteractor 
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public List<ProvineDto> Data { get; set; }
        }
        public class DetailProvineResponse
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public ProvineDto? Provine { get; set; }
        }
        Task<IGetProvineInteractor.Response> GetAsync();
        Task<IGetProvineInteractor.DetailProvineResponse> GetDetailProvineAsync(string provineId);
    }
    public class GetProvineInteractor : IGetProvineInteractor
    {
        private readonly ILocationService locationService;
        public GetProvineInteractor(ILocationService locationService)
        {
            this.locationService = locationService;
        }
        public async Task<IGetProvineInteractor.Response> GetAsync()
        {
            return await locationService.GetProvine();
        }
        public async Task<IGetProvineInteractor.DetailProvineResponse> GetDetailProvineAsync(string provineId)
        {
            return await locationService.GetDetailProvineAsync(provineId);
        }
    }
}
