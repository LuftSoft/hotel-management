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
        public HotelController(
            IUploadFileUtil uploadFileUtil,
            IJwtUtil jwtUtil,
            ICreateHotelInteractor createHotelInteractor,
            IUpdateHotelInteractor updateHotelInteractor
            ) 
        { 
            this.uploadFileUtil = uploadFileUtil;
            this.jwtUtil = jwtUtil;
            this.createHotelInteractor = createHotelInteractor;
            this.updateHotelInteractor = updateHotelInteractor;
        }
        [HttpGet()]
        public IActionResult get(int pageIndex, int pageSize, [FromQuery] GetListHotelFilterDto hotelFilterDto)
        {   
            return Ok(Directory.GetCurrentDirectory());
        }
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
        public IActionResult delete(int id)
        {
            return Ok(0);
        }
    }
}
