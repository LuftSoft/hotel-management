using hotel_management_api.APIs.Room.DTOs;
using hotel_management_api.Business.Interactor.Room;
using hotel_management_api.Business.Interactor.RoomGallery;
using hotel_management_api.Business.Services;
using hotel_management_api.Database.Repository;
using hotel_management_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_management_api.APIs.Room
{
    [Route("api/v{version:apiVersion}/room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IJwtUtil jwtUtil;
        private readonly IAddNewRoomInteractor addNewRoomInteractor;
        private readonly IUpdateRoomInteractor updateRoomInteractor;
        private readonly IDeleteRoomInteractor deleteRoomInteractor;
        private readonly IGetRoomByHotelIdInteractor getRoomByHotelIdInteractor;
        //room gallery
        private readonly ICreateRoomGalleryInteractor createRoomGalleryInteractor;
        private readonly IDeleteRoomGalleryInteractor deleteRoomGalleryInteractor;
        //test
        private readonly IRoomRepository roomRepository;
        private readonly IRoomGalleryService roomGalleryService;
        public RoomController
            (
            IJwtUtil jwtUtil,
            IAddNewRoomInteractor addNewRoomInteractor,
        IUpdateRoomInteractor updateRoomInteractor,
            IDeleteRoomInteractor deleteRoomInteractor,
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
            this.getRoomByHotelIdInteractor = getRoomByHotelIdInteractor;
            this.updateRoomInteractor = updateRoomInteractor;
            this.deleteRoomInteractor = deleteRoomInteractor;
            this.createRoomGalleryInteractor = createRoomGalleryInteractor;
            this.deleteRoomGalleryInteractor = deleteRoomGalleryInteractor;
            //test
            this.roomRepository = roomRepository;
            this.roomGalleryService = roomGalleryService;
        }
        // GET: api/<RoomController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RoomController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {   
            var room = await roomRepository.GetByHotelIdAsync(id);
            if(room == null)
            {
                return BadRequest("can't find room");
            }
            return Ok(room);
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
        // PUT api/<RoomController>/5
        [HttpPut("{id}")]
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

        // DELETE api/<RoomController>/5
        [HttpDelete("{id}")]
        [Authorize("admin")]
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
