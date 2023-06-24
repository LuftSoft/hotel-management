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
        private readonly IRoomRepository roomRepository;
        private readonly IHotelRepository hotelRepository;
        private readonly ICommentRepository commentRepository;
        private readonly IHomeletRepository homeletRepository;
        private readonly IProvineRepository provineRepository;
        private readonly IDistrictRepository districtRepository;
        private readonly IHotelBenefitRepository hotelBenefitRepository;
        private readonly IHotelCategoryRepository hotelCategoryRepository;
        public HotelService
            (
            IJwtUtil jwtUtil,
            IUserRepository userRepository,
            IUploadFileUtil uploadFileUtil,
            IRoomRepository roomRepository,
            IHotelRepository hotelRepository,
            ICommentRepository commentRepository,
            IHomeletRepository homeletRepository,
            IProvineRepository provineRepository,
            IDistrictRepository districtRepositor,
            IHotelBenefitRepository hotelBenefitRepository,
            IHotelCategoryRepository hotelCategoryRepository
            )
        {
            this.jwtUtil = jwtUtil;
            this.uploadFileUtil = uploadFileUtil;
            this.userRepository = userRepository;
            this.roomRepository = roomRepository;
            this.hotelRepository = hotelRepository;
            this.commentRepository = commentRepository;
            this.homeletRepository = homeletRepository;
            this.provineRepository = provineRepository;
            this.districtRepository = districtRepositor;
            this.hotelBenefitRepository = hotelBenefitRepository;
            this.hotelCategoryRepository = hotelCategoryRepository;
        }
        public async Task<HotelDto> ConvertHotelToHotelDto(Hotel hotel)
        {
            double min = Double.MaxValue, max = 0;
            var rooms = (await roomRepository.GetByHotelIdAsync(hotel.Id)).ToList();
            if (rooms.Any())
            {
                foreach( var room in rooms )
                {
                    if(room.Price < min) min = room.Price;
                    if (room.Price > max) max = room.Price;
                }
            }
            return new HotelDto()
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Star = hotel.Star,
                Description = hotel.Description,
                Address = hotel.Address,
                LogoLink = hotel.LogoLink,
                Slug = hotel.Slug,
                CreatedDate = hotel.CreatedDate,
                UpdateDate = hotel.UpdateDate,
                USerId = hotel.USerId,
                HotelCategoryId = hotel.HotelCategoryId,
                HotelCategory = hotel.HotelCategory,
                HotelBenefit = hotel.HotelBenefit,
                HomeletId = hotel.HomeletId,
                MinPrice = min,
                MaxPrice = max
            };
        }
        public async Task<IGetDetailHotelInteractor.Response> GetDetail(int hotelId)
        {
            Hotel hotel = await hotelRepository.FindByIdAsync(hotelId);
            if (hotel == null) return new IGetDetailHotelInteractor.Response("Get failed", false, null);
            var rooms = (await roomRepository.GetByHotelIdAsync(hotelId)).ToList();
            var comments = (await commentRepository.FindByHotelId(hotelId)).ToList();
            var benefit = await hotelBenefitRepository.FindByHotelIdAsync(hotelId);
            benefit.Hotel = null;
            var hotelCategory = await hotelCategoryRepository.GetById(hotel.HotelCategoryId);
            double avgRating = 0;
            foreach(var comment in comments)
            {
                avgRating += comment.Rating;
            }

            avgRating = Math.Round((avgRating / comments.Count)*2, MidpointRounding.AwayFromZero)/2;
            
            HotelDetailDto hotelDetailDto = new HotelDetailDto()
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Star = avgRating,
                Slug = hotel.Slug,
                USerId = hotel.USerId,
                Address = hotel.Address,
                LogoLink = hotel.LogoLink,
                HotelCategory = hotelCategory,
                UpdateDate = hotel.UpdateDate,
                CreatedDate = hotel.CreatedDate,
                Description = hotel.Description,
                Rooms = rooms,
                HotelBenefit = benefit,
                Comments = comments

            };
            return new IGetDetailHotelInteractor.Response("Get success", true, hotelDetailDto);
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
                    hotelFilter.RoonCount, hotelFilter.RoomSize);
                hotels = hotels.Where(h => containHotel.Contains(h.Id)).ToList();
                hotels = hotels.Skip(request.pageSize * request.pageIndex).Take(request.pageSize).ToList();
                var hotelDtos = new List<HotelDto>();
                foreach (var hotel in hotels)
                {
                    var tmp = await hotelBenefitRepository.FindByHotelIdAsync(hotel.Id);
                    hotel.HotelBenefit = tmp;
                    hotelDtos.Add(await ConvertHotelToHotelDto(hotel));
                }
                var category = await hotelCategoryRepository.GetAll();
                //tim kiem theo ngay`
                return new IGetListHotelInteractor.Response()
                {
                    Success = true,
                    Hotels = hotelDtos.ToList(),
                    TotalPage = (hotels.Count() / request.pageSize) + 1,
                    PageIndex = request.pageIndex,
                    Categories = (List<HotelCategory>?)category,
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
        public async Task<IGetListHotelFilterInteractor.Response> GetFilterPaging(IGetListHotelFilterInteractor.Request request)
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
                    hotelFilter.RoonCount, hotelFilter.RoomSize);
                hotels = hotels.Where(h => containHotel.Contains(h.Id)).ToList();
                var hotelDtos = new List<HotelDto>();
                foreach(var hotel in hotels)
                {
                    var tmpHotel = await ConvertHotelToHotelDto(hotel);
                    tmpHotel.HotelBenefit =  await hotelBenefitRepository.FindByHotelIdAsync(hotel.Id);
                    hotelDtos.Add(tmpHotel);
                }
                HotelFilterDto filterDto = request.filterDto;
                if (filterDto.PriceSort != null)
                {
                    switch (filterDto.PriceSort)
                    {
                        case 0:
                            hotelDtos = hotelDtos.OrderBy(h => h.MinPrice).ToList();
                            break;
                        case 1:
                            hotelDtos = hotelDtos.OrderByDescending(h => h.MaxPrice).ToList();
                            break;
                    }
                }
                if(filterDto.MaxPrice != null)
                {
                    hotelDtos = hotelDtos.Where(h => h.MaxPrice <= filterDto.MaxPrice).ToList();
                }
                if(filterDto.MinPrice != null)
                {
                    hotelDtos = hotelDtos.Where(h => h.MinPrice >= filterDto.MinPrice).ToList();
                }
                //benefit
                if(filterDto.WifiFree == true)
                {
                    hotelDtos = hotelDtos.Where(h => h.HotelBenefit.WifiFree == true).ToList();
                }
                if (filterDto.Resttaurant == true)
                {
                    hotelDtos = hotelDtos.Where(h => h.HotelBenefit.Resttaurant == true).ToList();
                }
                if (filterDto.AllTimeFrontDesk == true)
                {
                    hotelDtos = hotelDtos.Where(h => h.HotelBenefit.AllTimeFrontDesk == true).ToList();
                }
                if (filterDto.Elevator == true)
                {
                    hotelDtos = hotelDtos.Where(h => h.HotelBenefit.Elevator == true).ToList();
                }
                if (filterDto.Pool == true)
                {
                    hotelDtos = hotelDtos.Where(h => h.HotelBenefit.Pool == true).ToList();
                }
                if (filterDto.FreeBreakfast == true)
                {
                    hotelDtos = hotelDtos.Where(h => h.HotelBenefit.FreeBreakfast == true).ToList();
                }
                if (filterDto.AirConditioner == true)
                {
                    hotelDtos = hotelDtos.Where(h => h.HotelBenefit.AirConditioner == true).ToList();
                }
                if (filterDto.CarBorow == true)
                {
                    hotelDtos = hotelDtos.Where(h => h.HotelBenefit.CarBorow == true).ToList();
                }
                if (filterDto.Parking == true)
                {
                    hotelDtos = hotelDtos.Where(h => h.HotelBenefit.Parking == true).ToList();
                }
                if (filterDto.AllowPet == true)
                {
                    hotelDtos = hotelDtos.Where(h => h.HotelBenefit.AllowPet == true).ToList();
                }
                hotelDtos = hotelDtos.Skip(request.pageSize * request.pageIndex).Take(request.pageSize).ToList();
                var category = await hotelCategoryRepository.GetAll();
                //tim kiem theo ngay`
                return new IGetListHotelFilterInteractor.Response()
                {
                    Success = true,
                    Hotels = hotelDtos.ToList(),
                    TotalPage = (hotels.Count() / request.pageSize) + 1,
                    PageIndex = request.pageIndex,
                    Categories = (List<HotelCategory>?)category,
                    Message = "Get list hotel success"
                };
            }
            catch (Exception ex)
            {
                return new IGetListHotelFilterInteractor.Response()
                {
                    Success = false,
                    Message = "Get list filter failed"
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
