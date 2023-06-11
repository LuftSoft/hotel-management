using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.User
{
    public interface IDeleteUserInteractor
    {
        public class Request
        {
            public string token { get; set; }
            public string deleteId { get; set; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
        Task<IDeleteUserInteractor.Response> DeleteAsync(IDeleteUserInteractor.Request request);
    }
    public class DeleteUserInteractor: IDeleteUserInteractor
    {
        private readonly IUserService userService;
        public DeleteUserInteractor(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IDeleteUserInteractor.Response> DeleteAsync(IDeleteUserInteractor.Request request)
        {
            return await userService.DeleteAsync(request);
        }
    }
}
