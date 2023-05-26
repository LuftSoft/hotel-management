using hotel_management_api.APIs.Hotel.DTOs;
using hotel_management_api.Business.Interactor.Hotel;
using hotel_management_api.Database.Model;
using hotel_management_api.Database.Repository;
using hotel_management_api.Utils;

namespace hotel_management_api.Business.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IUploadFileUtil uploadFileUtil;
        private readonly IHotelBenefitRepository hotelBenefitRepository;
        private readonly IUserRepository userRepository;
        private readonly IJwtUtil jwtUtil;
        public HotelService(
            IHotelRepository hotelRepository,
            IUserRepository userRepository,
            IHotelBenefitRepository hotelBenefitRepository,
            IUploadFileUtil uploadFileUtil,
            IJwtUtil jwtUtil)
        {
            this.hotelRepository = hotelRepository;
            this.uploadFileUtil = uploadFileUtil;
            this.hotelBenefitRepository = hotelBenefitRepository;
            this.userRepository = userRepository;
            this.jwtUtil = jwtUtil;
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

    }
}
