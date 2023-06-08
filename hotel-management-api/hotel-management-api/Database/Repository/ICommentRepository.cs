using hotel_management_api.Database.Model;

namespace hotel_management_api.Database.Repository
{
    public interface ICommentRepository
    {
        Task<Comment?> FindByIdAsync(int id);
        Task<IEnumerable<Comment>?> FindByHotelId(int hotelId);
        Task<Comment?> CreateAsync(Comment comment);
        Task<bool> UpdateAsync(Comment comment);
        Task<bool> DeleteAsync(int id);

    }
}
