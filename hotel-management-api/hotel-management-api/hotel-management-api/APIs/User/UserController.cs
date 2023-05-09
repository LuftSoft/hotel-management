using Microsoft.AspNetCore.Mvc;

namespace hotel_management_api.APIs.User
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/user")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() 
        {
            return Ok("Get method");
        }
        [HttpPost]
        public IActionResult Post()
        {
            return Ok("post method");
        }
        [HttpPut]
        public IActionResult Put()
        {
            return Ok("put method");
        }
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("dedlete method");
        }
    }
}
