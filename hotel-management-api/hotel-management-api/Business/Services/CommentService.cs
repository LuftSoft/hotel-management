using hotel_management_api.APIs.Comment.DTOs;
using hotel_management_api.Business.Interactor.Comment;
using hotel_management_api.Database.Model;
using hotel_management_api.Database.Repository;

namespace hotel_management_api.Business.Services
{
    public class CommentService: ICommentService
    {
        private readonly IUserService userService; 
        private readonly IBookingRepository bookingRepository;
        private readonly ICommentRepository commentRepository;
        public CommentService
            (
            IUserService userService,
            IBookingRepository bookingRepository,
            ICommentRepository commentRepository
            )
        {
            this.userService = userService;
            this.bookingRepository = bookingRepository;
            this.commentRepository = commentRepository;
        }
        public async Task<ICreateCommentInteractor.Response> CreateAsync(ICreateCommentInteractor.Request request)
        {
            try
            {
                var isExist = await commentRepository.FindByBookingId(request.Comment.BookingId);
                if(isExist != null) 
                {
                    return new ICreateCommentInteractor.Response()
                    {
                        Success = false,
                        Message = "You already comment",
                    };
                }
                CommentDto commentDto = request.Comment;
                Comment comment = new Comment();
                comment.CreateDate = DateTime.Now;
                comment.LastChange = DateTime.Now;
                comment.Rating = commentDto.Rating;
                comment.Content = commentDto.Content;
                comment.BookingId = commentDto.BookingId;
                var success = await commentRepository.CreateAsync(comment);
                return new ICreateCommentInteractor.Response()
                {
                    Success = true,
                    Message = "Create comment success"
                };
            }
            catch (Exception ex)
            {
                return new ICreateCommentInteractor.Response()
                {
                    Success = false,
                    Message = "Create comment failed",
                };
            }
        }
        public async Task<IUpdateCommentInteractor.Response> UpdateAsync(IUpdateCommentInteractor.Request request)
        {
            Comment comment = await commentRepository.FindByIdAsync((int) request.Comment.Id);
            if(comment == null)
            {
                return new IUpdateCommentInteractor.Response()
                {
                    Success = false,
                    Message = "Update new comment failed"
                };
            }
            string userId = await userService.GetUserIdFromToken(request.token);
            if(userId == null)
            {
                return new IUpdateCommentInteractor.Response()
                {
                    Success = false,
                    Message = "Don't have permission to update comment information"
                };
            }
            var userBooking = await bookingRepository.GetOne(comment.BookingId);
            if (userId != userBooking.UserId)
            {
                return new IUpdateCommentInteractor.Response()
                {
                    Success = false,
                    Message = "You are not owner of this comment"
                };
            }
            comment.LastChange = DateTime.Now;
            comment.Rating = request.Comment.Rating;
            comment.Content = request.Comment.Content;
            await commentRepository.UpdateAsync(comment);
            return new IUpdateCommentInteractor.Response()
            {
                Success = true,
                Message = "Update new comment success"
            };
        }
        public async Task<IDeleteCommentInteractor.Response> DeleteAsync(IDeleteCommentInteractor.Request request)
        {
            Comment comment = await commentRepository.FindByIdAsync((int)request.CommentId);
            if(comment == null)
            {
                return new IDeleteCommentInteractor.Response()
                {
                    Success = false,
                    Message = "Delete comment failed, comment is not exists"
                };
            }
            //
            string userId = await userService.GetUserIdFromToken(request.token);
            if (userId == null)
            {
                return new IDeleteCommentInteractor.Response()
                {
                    Success = false,
                    Message = "Don't have permission to update comment information"
                };
            }
            var userBooking = await bookingRepository.GetOne(comment.BookingId);
            //
            if (userId != userBooking.UserId)
            {
                return new IDeleteCommentInteractor.Response()
                {
                    Success = false,
                    Message = "You are not owner of this comment"
                };
            }
            await commentRepository.DeleteAsync(request.CommentId);
            return new IDeleteCommentInteractor.Response()
            {
                Success = true,
                Message = "Delete comment success"
            };
        }
    }
}
