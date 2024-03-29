﻿using hotel_management_api.APIs.Comment.DTOs;
using hotel_management_api.APIs.Location.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Location
{
    public interface IGetDistrictInteractor 
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public string ProvineId { get; set; }
            public List<DistrictDto> Data { get; set; }
        }
        public class DetailDistrictResponse
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public DistrictDto? District { get; set; }
        }
        Task<IGetDistrictInteractor.Response> GetAsync(string provineId);
        Task<IGetDistrictInteractor.DetailDistrictResponse> GetDetailDistrictAsync(string districtId);
    }
    public class GetDistrictInteractor: IGetDistrictInteractor
    {
        private readonly ILocationService locationService;
        public GetDistrictInteractor(ILocationService locationService)
        {
            this.locationService = locationService;
        }
        public async Task<IGetDistrictInteractor.Response> GetAsync(string provineId)
        {
            return await locationService.GetDistrict(provineId);
        }
        public async Task<IGetDistrictInteractor.DetailDistrictResponse> GetDetailDistrictAsync(string districtId)
        {
            return await locationService.GetDetailDistrictAsync(districtId);
        }
    }
}
