using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.User
{
    public interface IDeleteUserInteractor
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
    }
    public class DeleteUserInteractor
    {
        private readonly IUserService userService;
        public DeleteUserInteractor(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IDeleteUserInteractor.Response> Delete(string userId)
        {
            return await userService.DeleteAsync(userId);
        }
    }
}
