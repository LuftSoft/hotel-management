using hotel_management_api.APIs.Location.DTOs;
using hotel_management_api.Business.Interactor.Location;
using hotel_management_api.Database.Model;
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
        public async Task<IGetHomeletInteractor.DetailHomeletResponse> GetDetailHomeletAsync(string homeletId)
        {
            Homelet result = await homeletRepository.FindByIdAsync(homeletId);
            if(result == null)
            {
                return new IGetHomeletInteractor.DetailHomeletResponse()
                {
                    Success = false,
                    Message = "Homelet is not exists!"
                };
            }
            return new IGetHomeletInteractor.DetailHomeletResponse()
            {
                Success = true,
                Message = "Get homelet successful!",
                Homelet = new HomeletDto()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Type = result.Type
                }
            };
        }
        public async Task<IGetDistrictInteractor.DetailDistrictResponse> GetDetailDistrictAsync(string districtId)
        {
            District result = await districtRepository.FindByIdAsync(districtId);
            if (result == null)
            {
                return new IGetDistrictInteractor.DetailDistrictResponse()
                {
                    Success = false,
                    Message = "Homelet is not exists!"
                };
            }
            return new IGetDistrictInteractor.DetailDistrictResponse()
            {
                Success = true,
                Message = "Get homelet successful!",
                District = new DistrictDto()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Type = result.Type
                }
            };
        }
        public async Task<IGetProvineInteractor.DetailProvineResponse> GetDetailProvineAsync(string provineId)
        {
            Provine result = await provineRepository.FindByIdAsync(provineId);
            if (result == null)
            {
                return new IGetProvineInteractor.DetailProvineResponse()
                {
                    Success = false,
                    Message = "Homelet is not exists!"
                };
            }
            return new IGetProvineInteractor.DetailProvineResponse()
            {
                Success = true,
                Message = "Get homelet successful!",
                Provine = new ProvineDto()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Type = result.Type
                }
            };
        }
    }
}
