using hotel_management_api.APIs.Room.DTOs;
using hotel_management_api.Business.Interactor.Room;
using hotel_management_api.Business.Interactor.RoomGallery;
using hotel_management_api.Business.Services;
using hotel_management_api.Database.Repository;
using hotel_management_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static hotel_management_api.Extension.Middlewares.IsUserBlockMiddleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_management_api.APIs.Room
{
    [Route("api/v{version:apiVersion}/room")]
    [ApiVersion("1.0")]
    [ApiController]
    [TypeFilter(typeof(hotelfilter))]
    public class RoomController : ControllerBase
    {
        private readonly IJwtUtil jwtUtil;
        private readonly IRoomRepository roomRepository;
        private readonly IRoomGalleryService roomGalleryService;
        private readonly IAddNewRoomInteractor addNewRoomInteractor;
        private readonly IUpdateRoomInteractor updateRoomInteractor;
        private readonly IDeleteRoomInteractor deleteRoomInteractor;
        private readonly IGetRoomByIdInteractor getRoomByIdInteractor;
        private readonly IGetRoomByHotelIdInteractor getRoomByHotelIdInteractor;
        private readonly ICreateRoomGalleryInteractor createRoomGalleryInteractor;
        private readonly IDeleteRoomGalleryInteractor deleteRoomGalleryInteractor;  
        public RoomController
            (
            IJwtUtil jwtUtil,
        IUpdateRoomInteractor updateRoomInteractor,
            IAddNewRoomInteractor addNewRoomInteractor,
            IDeleteRoomInteractor deleteRoomInteractor,
            IGetRoomByIdInteractor getRoomByIdInteractor,
            IGetRoomByHotelIdInteractor getRoomByHotelIdInteractor,
            ICreateRoomGalleryInteractor createRoomGalleryInteractor,
            IDeleteRoomGalleryInteractor deleteRoomGalleryInteractor,
            //
            IRoomRepository roomRepository,
            IRoomGalleryService roomGalleryService
            )
        {
            this.jwtUtil = jwtUtil;
            this.addNewRoomInteractor = addNewRoomInteractor;
            this.updateRoomInteractor = updateRoomInteractor;
            this.deleteRoomInteractor = deleteRoomInteractor;
            this.getRoomByIdInteractor = getRoomByIdInteractor;
            this.getRoomByHotelIdInteractor = getRoomByHotelIdInteractor;
            this.createRoomGalleryInteractor = createRoomGalleryInteractor;
            this.deleteRoomGalleryInteractor = deleteRoomGalleryInteractor;
            //test
            this.roomRepository = roomRepository;
            this.roomGalleryService = roomGalleryService;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await getRoomByIdInteractor.GetByIdAsync(id);
            if(result.Success == true) 
                return Ok(result);
            return BadRequest(result);
        }

        // POST api/<RoomController>
        [HttpPost]
        [Authorize("admin")]
        public async Task<IActionResult> Post([FromForm] CreateRoomDto createRoomDto)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            var result = await addNewRoomInteractor.Create(new IAddNewRoomInteractor.Request(createRoomDto, token));
            if(result.Success == true )
            {
                return Ok(result);  
            }
            return BadRequest(result);
        }
        [Authorize("admin")]
        [HttpPost("gallery")]
        public async Task<IActionResult> PostRoomImage([FromForm] CreateRoomGalleryDto dto)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null)
            {
                return BadRequest(new IDeleteRoomGalleryInteractor.Response()
                {
                    Success = false,
                    Message = "token is null"
                });
            }
            var result = await createRoomGalleryInteractor.Create(new ICreateRoomGalleryInteractor.Request()
            {
                token = token,
                RoomId = dto.RoomId,
                dto = dto.Images
            });
            if(result.Success == true )
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [Authorize("admin")]
        [HttpDelete("gallery")]
        public async Task<IActionResult> DeleteRoomImage([FromBody] IDeleteRoomGalleryInteractor.Request request)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if(token == null)
            {
                return BadRequest(new IDeleteRoomGalleryInteractor.Response()
                {
                    Success = false,
                    Message = "token is null"
                });
            }
            request.token = token;
            var result = await deleteRoomGalleryInteractor.Delete(request);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        // PUT
        [HttpPut]
        [Authorize("admin")]
        public async Task<IActionResult> Put([FromBody] RoomUpdateDto updateRoomDto)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null)
            {
                return BadRequest(new IDeleteRoomGalleryInteractor.Response()
                {
                    Success = false,
                    Message = "token is null"
                });
            }
            var result = await updateRoomInteractor.Update(new IUpdateRoomInteractor.Request()
            {
                roomUpdateDto = updateRoomDto,
                token = token
            });
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        // DELETE
        [Authorize("admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null)
            {
                return BadRequest(new IDeleteRoomGalleryInteractor.Response()
                {
                    Success = false,
                    Message = "token is null"
                });
            }
            var result = await deleteRoomInteractor.Delete(new IDeleteRoomInteractor.Request()
            {
                id = id,
                token = token
            });
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
