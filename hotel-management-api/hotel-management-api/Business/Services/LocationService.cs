using hotel_management_api.APIs.Location.DTOs;
using hotel_management_api.Business.Interactor.Location;
using hotel_management_api.Database.Repository;

namespace hotel_management_api.Business.Services
{
    public class LocationService:ILocationService
    {
        private readonly IHomeletRepository homeletRepository;
        private readonly IDistrictRepository districtRepository;
        private readonly IProvineRepository provineRepository;
        public LocationService
            (
            IHomeletRepository homeletRepository,
            IDistrictRepository districtRepository, 
            IProvineRepository provineRepository

            )
        {
            this.homeletRepository = homeletRepository;
            this.districtRepository = districtRepository;
            this.provineRepository = provineRepository;
        }
        public async Task<IGetHomeletInteractor.Response> GetHomelet(string districtId)
        {
            var homelets = (await homeletRepository.FindByDistrictIdAsync(districtId))
                .Select(h => new HomeletDto()
                {
                    Id = h.Id,
                    Name = h.Name,
                    Type = h.Type
                }).ToList();
            if(homelets == null)
            {
                return new IGetHomeletInteractor.Response()
                {
                    Success = false,
                    Message = "Get list homelet failed",
                    DistrictId = districtId
                };
            }
            return new IGetHomeletInteractor.Response()
            {
                Success = true,
                Message = "Get list homelet success",
                Data = homelets,
                DistrictId = districtId
            };
        }
        public async Task<IGetDistrictInteractor.Response> GetDistrict(string provineId)
        {
            var districts = (await districtRepository.FindByProvineIdAsync(provineId))
                .Select(h => new DistrictDto()
                {
                    Id = h.Id,
                    Name = h.Name,
                    Type = h.Type
                }).ToList();
            if(districts == null)
            {
                return new IGetDistrictInteractor.Response()
                {
                    Success = false,
                    Message = "Get list district failed",
                    ProvineId = provineId
                };
            }
            return new IGetDistrictInteractor.Response()
            {
                Success = true,
                Message = "Get list district success",
                Data = districts,
                ProvineId = provineId
            };
        }
        public async Task<IGetProvineInteractor.Response> GetProvine()
        {
            var provines = (await provineRepository.GetListProvine())
                .Select(h => new ProvineDto()
                {
                    Id = h.Id,
                    Name = h.Name,
                    Type = h.Type
                }).ToList();
            if(provines == null)
            {
                return new IGetProvineInteractor.Response()
                {
                    Success = false,
                    Message = "Get list provine false"
                };
            }
            return new IGetProvineInteractor.Response()
            {
                Success = true,
                Message = "Get list provine success",
                Data = provines
            };
        }
    }
}
