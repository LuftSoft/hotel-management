using hotel_management_api.APIs.Comment.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Comment
{
    public interface IDeleteCommentInteractor
    {
        public class Request
        {
            public int CommentId { get; set; }
            public string token { set; get; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
        Task<IDeleteCommentInteractor.Response> DeleteAsync(IDeleteCommentInteractor.Request request);
    }
    public class DeleteCommentInteractor : IDeleteCommentInteractor
    {
        private readonly ICommentService commentService;
        public DeleteCommentInteractor(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        public async Task<IDeleteCommentInteractor.Response> DeleteAsync(IDeleteCommentInteractor.Request request)
        {
            return await this.commentService.DeleteAsync(request);
        }
    }
}
