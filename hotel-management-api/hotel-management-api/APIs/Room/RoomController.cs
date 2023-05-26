using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_management_api.APIs.Room
{
    [Route("api/v{version:apiVersion}/room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        // GET: api/<RoomController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RoomController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoomController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RoomController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RoomController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
