using hotel_management_api.APIs.User.DTOs;
using hotel_management_api.Business.Services;
using hotel_management_api.Database.Model;

namespace hotel_management_api.Business.Interactor.User
{
    public interface IAddRoleToUserInteractor
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public UserDto userDto { get; set; }
        }
        Task<IAddRoleToUserInteractor.Response> AddRoleToUser(string userId, string role);
    }
    public class AddRoleToUserInteractor : IAddRoleToUserInteractor
    {
        private readonly IUserService userService;
        public AddRoleToUserInteractor(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<IAddRoleToUserInteractor.Response> AddRoleToUser(string userId, string role)
        {
            if(role == DbUserRole.Admin || role == DbUserRole.User || role == DbUserRole.Owner)
            {
                return await userService.AddRoleToUser(userId, role);
            }
            return new IAddRoleToUserInteractor.Response 
            {
                Success = true,
                Message = "Add role to user failed interactor"
            };
        }
    }
}
