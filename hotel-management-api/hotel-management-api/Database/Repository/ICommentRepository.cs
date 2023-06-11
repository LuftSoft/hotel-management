using hotel_management_api.APIs.Comment.DTOs;
using hotel_management_api.Database.Model;

namespace hotel_management_api.Database.Repository
{
    public interface ICommentRepository
    {
        Task<bool> DeleteAsync(int id);
        Task<Comment?> FindByIdAsync(int id);
        Task<Comment> FindByBookingId(int id);
        Task<bool> UpdateAsync(Comment comment);
        Task<Comment?> CreateAsync(Comment comment);
        Task<IEnumerable<CommentDto>?> FindByHotelId(int hotelId);

    }
}
