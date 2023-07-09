using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using hotel_management_api.APIs.Hotel.DTOs;
using hotel_management_api.Business.Interactor.Hotel;
using hotel_management_api.Business.Services;
using hotel_management_api.Utils;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static hotel_management_api.Extension.Middlewares.IsUserBlockMiddleware;

namespace hotel_management_api.APIs.Hotel
{
    [Route("api/v{version:apiVersion}/hotel")]
    [ApiVersion("1.0")]
    [ApiController]
    [TypeFilter(typeof(hotelfilter))]
    public class HotelController : ControllerBase
    {
        private readonly IJwtUtil jwtUtil;
        private readonly IHotelService hotelService;
        private readonly IUploadFileUtil uploadFileUtil;
        private readonly ICreateHotelInteractor createHotelInteractor;
        private readonly IUpdateHotelInteractor updateHotelInteractor;
        private readonly IDeleteHotelInteractor deleteHotelInteractor;
        private readonly IGetListHotelInteractor getListHotelInteractor;
        private readonly IGetDetailHotelInteractor getDetailHotelInteractor;
        private readonly IGetListHotelFilterInteractor getListHotelFilterInteractor;
        private readonly IGetListHotelOfOwnerInteractor getListHotelOfOwnerInteractor;
        public HotelController(
            IJwtUtil jwtUtil,
            IHotelService hotelService,
            IUploadFileUtil uploadFileUtil,
            ICreateHotelInteractor createHotelInteractor,
            IUpdateHotelInteractor updateHotelInteractor,
            IDeleteHotelInteractor deleteHotelInteractor,
            IGetListHotelInteractor getListHotelInteractor,
            IGetDetailHotelInteractor getDetailHotelInteractor,
            IGetListHotelFilterInteractor getListHotelFilterInteractor,
            IGetListHotelOfOwnerInteractor getListHotelOfOwnerInteractor
            )
        {
            this.jwtUtil = jwtUtil;
            this.hotelService = hotelService;
            this.uploadFileUtil = uploadFileUtil;
            this.createHotelInteractor = createHotelInteractor;
            this.updateHotelInteractor = updateHotelInteractor;
            this.deleteHotelInteractor = deleteHotelInteractor;
            this.getListHotelInteractor = getListHotelInteractor;
            this.getDetailHotelInteractor = getDetailHotelInteractor;
            this.getListHotelFilterInteractor = getListHotelFilterInteractor;
            this.getListHotelOfOwnerInteractor = getListHotelOfOwnerInteractor;
        }
        [HttpGet]
        public async Task<IActionResult> get(int pageIndex, int pageSize, [FromQuery] GetListHotelFilterDto hotelFilterDto)
        {
            if (pageSize == 0) pageSize = 10;
            var result = await getListHotelInteractor.GetAsync(new IGetListHotelInteractor.Request()
            {
                dto = hotelFilterDto,
                pageIndex = pageIndex,
                pageSize = pageSize
            });
            if (result.Success == true)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("all")]
        public async Task<IActionResult> getAll(int pageIndex, int pageSize)
        {
            if (pageSize == 0) pageSize = 10;
            var result = await getListHotelInteractor.GetAllAsync(new IGetListHotelInteractor.Request()
            {
                pageIndex = pageIndex,
                pageSize = pageSize
            });
            if (result.Success == true)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getDetail(int id)
        {
            var result = await getDetailHotelInteractor.GetDetail(id);
            if (result.Success == true)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("user")]
        [Authorize("admin")]
        public async Task<IActionResult> getByOwner()
        {
            try
            {
                string token = jwtUtil.getTokenFromHeader(HttpContext);
                var result = await getListHotelOfOwnerInteractor.GetHotelOfOwner(token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("filter")]
        public async Task<IActionResult> postFilter(int pageIndex, int pageSize, [FromQuery] GetListHotelFilterDto getListHotelFilterDto,
            [FromBody] HotelFilterDto? hotelFilterDto)
        {
            var result = await getListHotelFilterInteractor.GetFilterPaging(new IGetListHotelFilterInteractor.Request()
            {
                dto = getListHotelFilterDto,
                filterDto = hotelFilterDto,
                pageIndex = pageIndex,
                pageSize = pageSize
            });
            if(result.Success = true)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost]
        [Authorize("admin")]
        public async Task<IActionResult> post([FromForm] HotelUpdateDto dto)
        {
            try
            {
                string token = jwtUtil.getTokenFromHeader(HttpContext);
                var result = await createHotelInteractor.Create(new ICreateHotelInteractor.Request(dto, token));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("approval")]
        [Authorize("owner")]
        public async Task<IActionResult> getListApprovalHotel()
        {
            try
            {
                var result = await hotelService.GetListApprovalHotel();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("approval/{id}")]
        [Authorize("owner")]
        public async Task<IActionResult> approval(int id) 
        {
            try
            {
                var result = await hotelService.ApprovalHotel(id);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpPut]
        [Authorize("admin")]
        public async Task<IActionResult> put([FromForm] HotelUpdateDto hotelUpdateDto)
        {
            try
            {
                string token = jwtUtil.getTokenFromHeader(HttpContext);
                var result = await updateHotelInteractor.Update(new IUpdateHotelInteractor.Request(hotelUpdateDto, token));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize("admin")]
        public async Task<IActionResult> delete(int id)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null)
                return BadRequest();
            var result = await deleteHotelInteractor.Delete(new IDeleteHotelInteractor.Request()
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
