using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_management_api.APIs.Comment
{
    [Route("api/v{version:apiVersion}/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        // GET: api/<CommentControllser>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CommentControllser>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CommentControllser>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CommentControllser>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CommentControllser>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
