using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Database.Repository;

namespace hotel_management_api.Business.Services
{
    public interface IUserService
    {
        Task<IUserLoginInteractor.Response> LoginService(IUserLoginInteractor.Request request);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(
            IUserRepository userRepository
            ) 
        {
            this.userRepository = userRepository;
        }
        public async Task<IUserLoginInteractor.Response> LoginService(IUserLoginInteractor.Request request)
        {
            return await userRepository.LoginRepository(request.loginDto);
        }
    }
}
