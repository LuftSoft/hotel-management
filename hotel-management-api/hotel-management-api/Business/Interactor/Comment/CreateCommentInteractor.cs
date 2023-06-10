using hotel_management_api.APIs.Booking.DTOs;
using hotel_management_api.APIs.Comment.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.Comment
{
    public interface ICreateCommentInteractor
    {
        public class Request
        {
            public CommentDto? Comment { set; get; }
            public string token { set; get; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public CommentDto? Comment { set; get; }
        }
        Task<ICreateCommentInteractor.Response> CreateAsync(ICreateCommentInteractor.Request request);
    }
    public class CreateCommentInteractor : ICreateCommentInteractor
    {
        private readonly ICommentService commentService;
        public CreateCommentInteractor(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        public async Task<ICreateCommentInteractor.Response> CreateAsync(ICreateCommentInteractor.Request request)
        {
            return await this.commentService.CreateAsync(request);
        }
    }
}
