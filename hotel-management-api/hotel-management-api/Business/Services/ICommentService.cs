using hotel_management_api.Business.Interactor.Comment;

namespace hotel_management_api.Business.Services
{
    public interface ICommentService
    {
        Task<ICreateCommentInteractor.Response> CreateAsync(ICreateCommentInteractor.Request request);
        Task<IUpdateCommentInteractor.Response> UpdateAsync(IUpdateCommentInteractor.Request request);
        Task<IDeleteCommentInteractor.Response> DeleteAsync(IDeleteCommentInteractor.Request request);
    }
}
