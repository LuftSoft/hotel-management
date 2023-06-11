using hotel_management_api.APIs.Room.DTOs;
using hotel_management_api.Business.Interactor.Hotel;
using hotel_management_api.Business.Interactor.Room;
using hotel_management_api.Database;
using hotel_management_api.Database.Model;
using hotel_management_api.Database.Repository;
using hotel_management_api.Utils;

namespace hotel_management_api.Business.Services
{
    public class RoomService : IRoomService
    {
        private readonly ILogger logger;
        private readonly IRoomRepository roomRepository;
        private readonly IRoomGalleryRepository roomGalleryRepository;
        private readonly IRoomGalleryService roomGalleryService;
        private readonly IUserService userService;
        private readonly IHotelRepository hotelRepository;
        private readonly IUploadFileUtil uploadFileUtil;
        public RoomService
            (
            ILogger logger,
            IUserService userService,
            IUploadFileUtil uploadFileUtil,
            IRoomRepository roomRepository,
            IRoomGalleryService roomGalleryService,
            IRoomGalleryRepository roomGalleryRepository,
            IHotelRepository hotelRepository
            ) 
        {   
            this.logger = logger;
            this.userService = userService; 
            this.uploadFileUtil = uploadFileUtil;
            this.roomRepository = roomRepository;
            this.roomGalleryService = roomGalleryService;
            this.roomGalleryRepository = roomGalleryRepository;
            this.hotelRepository = hotelRepository;
        }
        //convert
        private RoomDetailDto ConvertToRoomDetailDto(Room room, List<RoomGalleryDto> roomGalleryDtos)
        {
            return new RoomDetailDto()
            {
                Id = room.Id,
                Name = room.Name,
                NumOfPeope = room.NumOfPeope,
                NumOfBed = room.NumOfBed,
                TypeOfBed = room.TypeOfBed,
                Description = room.Description,
                Price = room.Price,
                Square = room.Square,
                //So luong phong nhu nay la bao nhieu
                TotalRoom = room.TotalRoom,
                //hoa`n tie`n   
                Refund = room.Refund,
                //do?i li.ch
                Reschedule = room.Reschedule,
                HotelId = room.HotelId,
                HotelImageGalleries = roomGalleryDtos
            };
        }
        public async Task<IGetRoomByIdInteractor.Response> GetByIdAsync(int id)
        {
            Room room = await roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                return new IGetRoomByIdInteractor.Response()
                {
                    Success = false,
                    Message = "Get detail room failed"
                };
            }
            List<RoomGalleryDto> roomGalleryDtos = (await roomGalleryService.GetByRoomId(id)).ToList();
            return new IGetRoomByIdInteractor.Response()
            {
                Success = true,
                Message = "Get detail room success",
                RoomDetailDto = ConvertToRoomDetailDto(room, roomGalleryDtos)
            };
        }
        public bool GetByHotelId()
        {
            return false;
        }
        public async Task<string> GetUserId(int roomId)
        {
            return await roomRepository.GetUserIdAsync(roomId);
        }
        public async Task<IAddNewRoomInteractor.Response> Create(IAddNewRoomInteractor.Request request)
        {
            try
            {
                var roomDto = request.dto;
                var useridIntoken = await userService.GetUserIdFromToken(request.token);
                var userId = (await hotelRepository.getOne(request.dto.HotelId)).USerId;
                if (userId != useridIntoken)
                {
                    return new IAddNewRoomInteractor.Response("You don't have permission to create this room", false);
                }
                Room room = new Room();
                room.Name = roomDto.Name;
                room.NumOfPeope = roomDto.NumOfPeope;
                room.NumOfBed = roomDto.NumOfBed;
                room.TypeOfBed = roomDto.TypeOfBed;
                room.Description = roomDto.Description;
                room.Price = roomDto.Price;
                room.TotalRoom = roomDto.TotalRoom;
                room.Refund = roomDto.Refund;
                room.Reschedule = roomDto.Reschedule;
                room.CreatedDate = DateTime.Now;
                room.UpdateDate = DateTime.Now;
                room.HotelId = roomDto.HotelId;
                var roomResult = await roomRepository.CreateAsync(room);
                Console.WriteLine("ROOM ID: " + roomResult.Id);
                var uploadResult = await uploadFileUtil.MultiUploadAsync(roomDto.Images.ToArray());
                foreach (string item in uploadResult)
                {
                    RoomGallery roomGallery = new RoomGallery();
                    roomGallery.RooomId = room.Id;
                    roomGallery.Link = item;
                    await roomGalleryRepository.CreateAsync(roomGallery);
                }
                return new IAddNewRoomInteractor.Response("Create room success", true);
            }catch(Exception ex)
            {
                logger.LogInformation($"Create room service failed. Error: {ex.Message}");
                return new IAddNewRoomInteractor.Response("Create room failed", false);
            }
        }
        public async Task<IUpdateRoomInteractor.Response> Update(IUpdateRoomInteractor.Request request)
        {
            string userIdFromRoom = await GetUserId(request.roomUpdateDto.Id);
            string userIdFromToken = await userService.GetUserIdFromToken(request.token);
            if(userIdFromRoom != userIdFromToken)
            {
                return new IUpdateRoomInteractor.Response() 
                {
                    Success = false,
                    Message = "You don't have permission to edit information of this room"
                };
            }
            Room room = await roomRepository.GetByIdAsync(request.roomUpdateDto.Id);
            room.UpdateDate = DateTime.Now;
            room.Name = request.roomUpdateDto.Name;
            room.NumOfPeope = request.roomUpdateDto.NumOfPeope ;
            room.NumOfBed = request.roomUpdateDto.NumOfBed ;
            room.TypeOfBed = request.roomUpdateDto.TypeOfBed ;
            room.Description = request.roomUpdateDto.Description;
            room.Price = request.roomUpdateDto.Price ;
            room.Square = request.roomUpdateDto.Square ;
            room.TotalRoom = request.roomUpdateDto.TotalRoom ;
            room.Refund = request.roomUpdateDto.Refund ;
            room.Reschedule = request.roomUpdateDto.Reschedule;
            bool isSuccess = await roomRepository.UpdateAsync(room);
            if(isSuccess)
            {
                return new IUpdateRoomInteractor.Response()
                {
                    Success = true,
                    Message = "Update success"
                };
            }
            return new IUpdateRoomInteractor.Response()
            {
                Success = false,
                Message = "Update failed"
            };
        }

        public async Task<IDeleteRoomInteractor.Response> Delete(IDeleteRoomInteractor.Request request)
        {
            string userId = await userService.GetUserIdFromToken(request.token);
            string userRoomId = await GetUserId(request.id);
            if(userId != userRoomId)
            {
                return new IDeleteRoomInteractor.Response()
                {
                    Success = false,
                    Message = "You don't have permission to delete this room"
                };
            }
            var isSuccess = await roomRepository.DeleteAsync(request.id);
            if(isSuccess)
            {
                return new IDeleteRoomInteractor.Response()
                {
                    Success = true,
                    Message = "Delete room success"
                };
            }
            return new IDeleteRoomInteractor.Response()
            {
                Success = false,
                Message = "Delete room failed"
            };
        }
    }
}
