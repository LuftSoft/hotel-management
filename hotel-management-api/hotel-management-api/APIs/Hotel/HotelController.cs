using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using hotel_management_api.APIs.Hotel.DTOs;
using hotel_management_api.Business.Interactor.Hotel;
using hotel_management_api.Utils;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotel_management_api.APIs.Hotel
{
    [Route("api/v{version:apiVersion}/hotel")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUploadFileUtil uploadFileUtil;
        private readonly IJwtUtil jwtUtil;
        private readonly ICreateHotelInteractor createHotelInteractor;
        private readonly IUpdateHotelInteractor updateHotelInteractor;
        private readonly IDeleteHotelInteractor deleteHotelInteractor;
        private readonly IGetListHotelInteractor getListHotelInteractor;
        public HotelController(
            IJwtUtil jwtUtil,
            IUploadFileUtil uploadFileUtil,
            ICreateHotelInteractor createHotelInteractor,
            IUpdateHotelInteractor updateHotelInteractor,
            IDeleteHotelInteractor deleteHotelInteractor,
            IGetListHotelInteractor getListHotelInteractor
            ) 
        { 
            this.jwtUtil = jwtUtil;
            this.uploadFileUtil = uploadFileUtil;
            this.createHotelInteractor = createHotelInteractor;
            this.updateHotelInteractor = updateHotelInteractor;
            this.deleteHotelInteractor = deleteHotelInteractor;
            this.getListHotelInteractor = getListHotelInteractor;
        }
        [HttpGet()]
        public async Task<IActionResult> get(  int pageIndex, int pageSize, [FromQuery] GetListHotelFilterDto hotelFilterDto)
        {   
            //if(hotelFilterDto.RoonCount < 0 || hotelFilterDto.FromDate < )
            Console.WriteLine($"{pageIndex}{pageSize}");
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
        [HttpGet("getsuggest")]
        public IActionResult getSuggestHotelPosition(string? provine)
        {
            return Ok(Directory.GetCurrentDirectory());
        }
        [HttpPost("filter")]
        public IActionResult postFilter(int pageIndex, int pageSize, [FromQuery] HotelFilterDto? hotelFilterDto)
        {
            return Ok(Directory.GetCurrentDirectory());
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
        [Authorize("admin")]
        [HttpPost("upload")]
        public async Task<IActionResult> testFile(IFormFile[] ufile)
        {
            try
            {
                if (ufile != null && ufile.Length > 0)
                {
                    var result = await uploadFileUtil.MultiUploadAsync(ufile);
                    return Ok(result);
                }
                return BadRequest(false);
            }
            catch
            {
                return BadRequest(Directory.GetCurrentDirectory() + "\\wwwroot\\upload");
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
        [HttpDelete]
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
