using hotel_management_api.APIs.Hotel.DTOs;
using hotel_management_api.Business.Interactor.Hotel;
using hotel_management_api.Database.Model;
using hotel_management_api.Database.Repository;
using hotel_management_api.Utils;
using System.Linq;

namespace hotel_management_api.Business.Services
{
    public class HotelService : IHotelService
    {
        private readonly IJwtUtil jwtUtil;
        private readonly IUploadFileUtil uploadFileUtil;
        private readonly IUserRepository userRepository;
        private readonly IHotelRepository hotelRepository;
        private readonly IHomeletRepository homeletRepository;
        private readonly IProvineRepository provineRepository;
        private readonly IDistrictRepository districtRepository;
        private readonly IHotelBenefitRepository hotelBenefitRepository;
        public HotelService
            (
            IJwtUtil jwtUtil,
            IUserRepository userRepository,
            IUploadFileUtil uploadFileUtil,
            IHotelRepository hotelRepository,
            IHomeletRepository homeletRepository,
            IProvineRepository provineRepository,
            IDistrictRepository districtRepositor,
            IHotelBenefitRepository hotelBenefitRepository
            )
        {
            this.jwtUtil = jwtUtil;
            this.uploadFileUtil = uploadFileUtil;
            this.userRepository = userRepository;
            this.hotelRepository = hotelRepository;
            this.homeletRepository = homeletRepository;
            this.provineRepository = provineRepository;
            this.districtRepository = districtRepositor;
            this.hotelBenefitRepository = hotelBenefitRepository;
        }
        public async Task<IGetListHotelInteractor.Response> GetPaging(IGetListHotelInteractor.Request request)
        {
            try
            {
                var hotelFilter = request.dto;
                var hotels = await hotelRepository.GetAllAsync();
                if (hotelFilter.HomeletId != null)
                {
                    hotels = hotels.Where(h => h.HomeletId == hotelFilter.HomeletId).ToList();
                }
                else if (hotelFilter.DistrictId != null)
                {
                    var homelets = await homeletRepository.FindByDistrictIdAsync(hotelFilter.DistrictId);
                    if (homelets != null)
                    {
                        List<string> homeletsId = homelets.Select(h => h.Id).ToList();
                        hotels = hotels.Where(h => homeletsId.Contains(h.HomeletId)).ToList();
                    }
                }
                else if (hotelFilter.ProvineId != null)
                {
                    var districts = await districtRepository.FindByProvineIdAsync(hotelFilter.ProvineId);
                    List<string> homeletIds = new List<string>();
                    if (districts != null)
                    {
                        foreach (var item in districts)
                        {
                            var tmp = (await homeletRepository.FindByDistrictIdAsync(item.Id)).Select(h => h.Id).ToList();
                            homeletIds.AddRange(tmp);
                        }
                    }
                    hotels = hotels.Where(h => homeletIds.Contains(h.HomeletId)).ToList();
                }
                var containHotel = await hotelRepository.HotelFilterAsync(hotelFilter.FromDate,
                    hotelFilter.RoonCount, hotelFilter.RoomSize, hotelFilter.Price);
                hotels = hotels.Where(h => containHotel.Contains(h.Id)).ToList();
                hotels = hotels.Skip(request.pageSize * request.pageIndex).Take(request.pageSize).ToList();
                //tim kiem theo ngay`
                return new IGetListHotelInteractor.Response()
                {
                    Success = true,
                    Hotels = hotels.ToList(),
                    TotalPage = (hotels.Count() / request.pageSize)+1,
                    PageIndex = request.pageIndex,
                    Message = "Get list hotel success"
                };
            }
            catch (Exception ex)
            {
                return new IGetListHotelInteractor.Response()
                {
                    Success = false,
                    Message = $"Get list hotel failed!.Hotel service. Error: {ex.Message}"
                };
            }
        }
        public async Task<ICreateHotelInteractor.Response> Create(ICreateHotelInteractor.Request request)
        {
            var userName = jwtUtil.getUserNameFromToken(request.token);
            var userId = (await userRepository.findUserByEmailAsync(userName)).Id;
            var dto = request.dto;
            var fileString = await uploadFileUtil.UploadAsync(dto.Logo);
            var htBn = new HotelBenefit()
            {
                AirConditioner = dto.AirConditioner,
                AllowPet = dto.AllowPet,
                AllTimeFrontDesk = dto.AllTimeFrontDesk,
                CarBorow = dto.CarBorow,
                Elevator = dto.Elevator,
                FreeBreakfast = dto.FreeBreakfast,
                Parking = dto.Parking,
                Pool = dto.Pool,
                Resttaurant = dto.Resttaurant,
                WifiFree = dto.WifiFree
            };
            var hotelBenefitResultId = await hotelBenefitRepository.CreateAsync(htBn);
            Hotel hotel = new Hotel()
            {
                Name = dto.Name,
                Address = dto.Address,
                Description = dto.Description,
                CreatedDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                HomeletId = dto.HomeletId,
                LogoLink = fileString,
                Star = 5.0,
                HotelCategoryId = dto.HotelCategoryId,
                Slug = dto.Slug,
                USerId = userId,
                HotelBenefitId = hotelBenefitResultId
            };
            var hotelResult = await hotelRepository.createAsync(hotel);
            if (hotelResult == null)
                return new ICreateHotelInteractor.Response("can't create hotel, error occur", false);
            return new ICreateHotelInteractor.Response("create hotel success", true);
        }
        public async Task<IUpdateHotelInteractor.Response> Update(IUpdateHotelInteractor.Request request)
        {
            var userName = jwtUtil.getUserNameFromToken(request.token);
            var userId = (await userRepository.findUserByEmailAsync(userName)).Id;
            var dto = request.dto;
            if (userId != dto.USerId)
            {
                return new IUpdateHotelInteractor.Response("you are not owner of the hotel", true);
            }
            var fileString = "";
            if (dto.Logo != null)
            {
                fileString = await uploadFileUtil.UploadAsync(dto.Logo);
            }
            var htBn = await hotelBenefitRepository.FindByIdAsync(dto.HotelBenefitId);
            htBn.AirConditioner = dto.AirConditioner;
            htBn.AllowPet = dto.AllowPet;
            htBn.AllTimeFrontDesk = dto.AllTimeFrontDesk;
            htBn.CarBorow = dto.CarBorow;
            htBn.Elevator = dto.Elevator;
            htBn.FreeBreakfast = dto.FreeBreakfast;
            htBn.Parking = dto.Parking;
            htBn.Pool = dto.Pool;
            htBn.Resttaurant = dto.Resttaurant;
            htBn.WifiFree = dto.WifiFree;
            await hotelBenefitRepository.UpdateAsync(htBn);
            var hotelResult = await hotelRepository.FindByIdAsync(dto.Id);
            if (hotelResult == null)
                return new IUpdateHotelInteractor.Response("update hotel information failed", false);
            hotelResult.Name = dto.Name;
            hotelResult.Address = dto.Address;
            hotelResult.Description = dto.Description;
            hotelResult.UpdateDate = DateTime.Now;
            hotelResult.HomeletId = dto.HomeletId;
            hotelResult.Star = dto.Star;
            hotelResult.HotelCategoryId = dto.HotelCategoryId;
            hotelResult.Slug = dto.Slug;
            if (fileString != "") hotelResult.LogoLink = fileString;
            await hotelRepository.updateAsync(hotelResult);
            return new IUpdateHotelInteractor.Response("update hotel information success", true);
        }
        public async Task<IDeleteHotelInteractor.Response> Delete(IDeleteHotelInteractor.Request request)
        {
            var userName = jwtUtil.getUserNameFromToken(request.token);
            var userId = (await userRepository.findUserByEmailAsync(userName)).Id;
            var hotel = await hotelRepository.FindByIdAsync(request.id);
            if (userId != hotel.USerId)
            {
                return new IDeleteHotelInteractor.Response()
                {
                    Success = true,
                    Message = "you are not owner of the hotel"

                };
            }
            var isSuccess = await hotelRepository.deleteAsync(request.id);
            if (isSuccess)
            {
                return new IDeleteHotelInteractor.Response()
                {
                    Success = true,
                    Message = "Delete hotel success"

                };
            }
            return new IDeleteHotelInteractor.Response()
            {
                Success = false,
                Message = "Delete hotel failed"

            };
        }
    }
}
