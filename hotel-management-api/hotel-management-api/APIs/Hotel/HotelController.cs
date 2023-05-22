using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using hotel_management_api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotel_management_api.APIs.Hotel
{
    [Route("api/v{version:apiVersion}/hotel")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUploadFileUtil uploadFileUtil;
        public HotelController(IUploadFileUtil uploadFileUtil) 
        { 
            this.uploadFileUtil = uploadFileUtil;
        }
        [HttpGet]
        public IActionResult get()
        {   
            return Ok(Directory.GetCurrentDirectory());
        }
        [HttpPost]
        public IActionResult post()
        {
            return Ok();
        }
        [HttpPost("upload")]
        public async Task<IActionResult> testFile(IFormFile ufile)
        {
            try
            {
                if (ufile != null && ufile.Length > 0)
                {
                    //var fileName = Path.GetFileName(ufile.FileName);
                    //var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\upload", "image_upload"+DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")+".jpg");
                    //using (var fileStream = new FileStream(filePath, FileMode.Create))
                    //{
                    //    await ufile.CopyToAsync(fileStream);
                    //}
                    //Cloudinary cloud = new Cloudinary("cloudinary://278276873974371:uA82HjnDFeTVYnLLtdshkwouFcE@dnshdled2");
                    //cloud.Api.Secure = true;
                    //var uploadparam = new ImageUploadParams()
                    //{   
                    //    File = new FileDescription(filePath),
                    //    UseFilename = true,
                    //    Folder = "hotel_management",
                    //    UniqueFilename = true,
                    //    Overwrite = true
                    //};
                    //var result = cloud.Upload(uploadparam);
                    //if(System.IO.File.Exists(filePath))
                    //    System.IO.File.Delete(filePath); 
                    //return Ok(result.Url);
                    var result = await uploadFileUtil.Upload(ufile);
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
        public IActionResult put() 
        {
            return BadRequest();
        }
        [HttpDelete]
        public IActionResult delete(int id)
        {
            return Ok(0);
        }
    }
}
