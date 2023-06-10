using hotel_management_api.APIs.Comment.DTOs;
using hotel_management_api.Business.Interactor.Comment;
using hotel_management_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotel_management_api.APIs.Comment
{
    [Route("api/v{version:apiVersion}/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IJwtUtil jwtUtil;
        private readonly ICreateCommentInteractor createCommentInteractor;
        private readonly IUpdateCommentInteractor updateCommentInteractor;
        private readonly IDeleteCommentInteractor deleteCommentInteractor;
        public CommentController
            (
                IJwtUtil jwtUtil,
                ICreateCommentInteractor createCommentInteractor,
                IUpdateCommentInteractor updateCommentInteractor,
                IDeleteCommentInteractor deleteCommentInteractor
            )
        {
            this.jwtUtil = jwtUtil;
            this.createCommentInteractor = createCommentInteractor;
            this.updateCommentInteractor = updateCommentInteractor;
            this.deleteCommentInteractor = deleteCommentInteractor;
        }
        // POST api/<CommentControllser>
        [HttpPost]
        [Authorize("user")]
        public async Task<IActionResult> Post([FromBody] CommentDto commentDto)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if(token == null)
            {
                return BadRequest("Not authorized");
            }
            var result = await createCommentInteractor.CreateAsync(new ICreateCommentInteractor.Request() 
            {
                token = token,
                Comment = commentDto
            });
            if(result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // PUT api/<CommentControllser>/5
        [HttpPut]
        [Authorize("user")]
        public async Task<IActionResult> Put([FromBody] CommentDto commentDto)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null)
            {
                return BadRequest("Not authorized");
            }
            var result = await updateCommentInteractor.UpdateAsync(new IUpdateCommentInteractor.Request()
            {
                token = token,
                Comment = commentDto
            });
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // DELETE api/<CommentControllser>/5
        [HttpDelete("{id}")]
        [Authorize("user")]
        public async Task<IActionResult> Delete(int id)
        {
            string token = jwtUtil.getTokenFromHeader(HttpContext);
            if (token == null)
            {
                return BadRequest("Not authorized");
            }
            var result = await deleteCommentInteractor.DeleteAsync(new IDeleteCommentInteractor.Request()
            {
                token = token,
                CommentId = id
            });
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
