using hotel_management_api.APIs.User.DTOs;
using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.User
{
    public interface IGetAllUserInteractor
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public List<UserDto> Users { get; set; }
        }
        Task<IGetAllUserInteractor.Response> GetAllUser();
    }
    public class GetAllUserInteractor : IGetAllUserInteractor
    {
        private readonly IUserService userService;
        public GetAllUserInteractor(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IGetAllUserInteractor.Response> GetAllUser()
        {
            return await userService.GetAllUser();
        }
    }
}
