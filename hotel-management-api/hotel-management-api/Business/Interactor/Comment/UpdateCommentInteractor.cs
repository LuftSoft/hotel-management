using hotel_management_api.APIs.Comment.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Comment
{
    public interface IUpdateCommentInteractor
    {
        public class Request
        {
            public CommentDto Comment { get; set; }
            public string token { set; get; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
        Task<IUpdateCommentInteractor.Response> UpdateAsync(IUpdateCommentInteractor.Request request);
    }
    public class UpdateCommentInteractor: IUpdateCommentInteractor
    {
        private readonly ICommentService commentService;
        public UpdateCommentInteractor(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        public async Task<IUpdateCommentInteractor.Response> UpdateAsync(IUpdateCommentInteractor.Request request)
        {
            return await this.commentService.UpdateAsync(request);
        }
    }
}
