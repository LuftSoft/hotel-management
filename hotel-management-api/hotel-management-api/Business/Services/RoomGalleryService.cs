using hotel_management_api.Business.Interactor.RoomGallery;
using hotel_management_api.Database.Repository;
using hotel_management_api.Utils;

namespace hotel_management_api.Business.Services
{
    public class RoomGalleryService : IRoomGalleryService
    {
        private readonly IRoomGalleryRepository roomGalleryRepository;
        private readonly IUploadFileUtil uploadFileUtil;
        private readonly IUserRepository userRepository;
        private readonly IRoomRepository roomRepository;
        private readonly IHotelRepository hotelRepository;
        private readonly IJwtUtil jwtUtil;
        private readonly ILogger logger;
        private readonly IUserService userService;
        public RoomGalleryService
            (
            IJwtUtil jwtUtil,
            ILogger logger,
            IUserRepository userRepository,
            IUserService userService,
            IUploadFileUtil uploadFileUtil,
            IRoomRepository roomRepository,
            IHotelRepository hotelRepository,
            IRoomGalleryRepository roomGalleryRepository
            )
        {
            this.jwtUtil = jwtUtil;
            this.logger = logger;
            this.userService = userService;
            this.userRepository = userRepository;
            this.uploadFileUtil = uploadFileUtil;
            this.roomRepository = roomRepository;
            this.hotelRepository = hotelRepository;
            this.roomGalleryRepository = roomGalleryRepository;
        }
        private async Task<bool> CheckAdmin(string token, int roomId)
        {
            string? userId = await userService.GetUserIdFromToken(token);
            if (userId == null)
            {
                logger.LogError($"RoomGalleryService-Create: User is null");
                return false;
            }
            var room = await roomRepository.GetByIdAsync(roomId);
            if (room == null)
            {
                logger.LogError($"RoomGalleryService-Create: Room is null");
                return false;
            }
            var hotel = await hotelRepository.getOne(room.HotelId);
            if (hotel == null)
            {
                logger.LogError($"RoomGalleryService-Create: Hotel is null");
                return false;
            }
            if (userId != hotel.USerId)
            {
                logger.LogError($"RoomGalleryService-Create: User don't have permission to change this information");
                return false;
            }
            return true;
        }
        public async Task<ICreateRoomGalleryInteractor.Response> Create(ICreateRoomGalleryInteractor.Request request)
        {
            var isAdmin = await CheckAdmin(request.token, request.RoomId);
            if (!isAdmin)
            {
                return new ICreateRoomGalleryInteractor.Response()
                {
                    Success = false,
                    Message = "User don't have permission to change this information"
                };
            }
            try
            {
                List<string> imageLink = (await uploadFileUtil.MultiUploadAsync(request.dto)).ToList();
                foreach (string item in imageLink)
                {
                    await roomGalleryRepository.CreateAsync(new Database.Model.RoomGallery()
                    {
                        Id = 0,
                        Link = item,
                        RooomId = request.RoomId
                    });
                }
                return new ICreateRoomGalleryInteractor.Response()
                {
                    Success = true,
                    Message = "Update room image success"
                }; 
            }
            catch (Exception ex)
            {
                logger.LogError($"RoomGalleryService-Create: {ex.Message}");
                return new ICreateRoomGalleryInteractor.Response()
                {
                    Success = false,
                    Message = $"Update failed, error: {ex.Message}"
                }; ;
            }
        }
        public async Task<IDeleteRoomGalleryInteractor.Response> Delete(IDeleteRoomGalleryInteractor.Request request)
        {
            int[] ids = request.dto;
            if(ids.Length == 0)
            {
                return new IDeleteRoomGalleryInteractor.Response()
                {
                    Success = false,
                    Message = "List delete room gallery is null"
                };
            }
            string userId = await userService.GetUserIdFromToken(request.token);
            if(userId == null)
            {
                return new IDeleteRoomGalleryInteractor.Response()
                {
                    Success = false,
                    Message = "user is null"
                };
            }
            var roomGallery = await roomGalleryRepository.GetByIdAsync(ids[0]);
            if(roomGallery == null)
            {
                return new IDeleteRoomGalleryInteractor.Response()
                {
                    Success = false,
                    Message = "one of roomgallery is not exists"
                };
            }
            int roomId = roomGallery.RooomId;
            for (int i = 1;i<ids.Length;i++)
            {
                roomGallery =  await roomGalleryRepository.GetByIdAsync(ids[i]);
                if (roomGallery == null)
                {
                    return new IDeleteRoomGalleryInteractor.Response()
                    {
                        Success = false,
                        Message = "one of room galleries is not exists"
                    };
                }
                if(roomGallery.RooomId != roomId)
                {
                    return new IDeleteRoomGalleryInteractor.Response()
                    {
                        Success = false,
                        Message = "room galleries is not in the same room"
                    };
                }
            }
            string compareUserId = await roomRepository.GetUserIdAsync(roomId);
            if (compareUserId == null || compareUserId != userId)
            {
                return new IDeleteRoomGalleryInteractor.Response()
                {
                    Success = true,
                    Message = "user don't have permission to delete gallery"
                };
            }
            for(int i=0; i< ids.Length; i++)
            {
                await roomGalleryRepository.DeleteAsync(ids[i]);
            }
            return new IDeleteRoomGalleryInteractor.Response()
            {
                Success = true,
                Message = "delete room galleries success"
            };
        }

        public async Task<IEnumerable<RoomGalleryDto>> GetByRoomId(int roomId)
        {
            var roomGalleries = await roomGalleryRepository.GetByRoomIdAsync(roomId);
            List<RoomGalleryDto> results = new List<RoomGalleryDto>();
            foreach(var item in roomGalleries)
            {
                results.Add(new RoomGalleryDto() 
                {
                    Id = item.Id,
                    RooomId = item.RooomId,
                    Link = item.Link
                });
            }
            return results;
        }
    }
}
