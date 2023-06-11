using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Business.Services;
using Microsoft.Identity.Client;

namespace hotel_management_api.Business.Interactor.User
{
    public interface IUpdateUserInteractor
    {
        public class Request
        {
            public UpdateUserDto updateUserDto { get; set; }
            public string token { get; set; }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
        }
        Task<IUpdateUserInteractor.Response> UpdateAsync(IUpdateUserInteractor.Request request);
    }
    public class UpdateUserInteractor: IUpdateUserInteractor
    {
        private readonly IUserService userService;
        public UpdateUserInteractor(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IUpdateUserInteractor.Response> UpdateAsync(IUpdateUserInteractor.Request request)
        {
            return await userService.UpdateAsync(request);
        }
    }
}
