using hotel_management_api.APIs.User.UserDTOs;
using hotel_management_api.Business.Boudaries.User;
using hotel_management_api.Database.Model;
using Microsoft.AspNetCore.Identity;

namespace hotel_management_api.Database.Repository
{   
    public interface IUserRepository
    {
        public Task<IUserLoginInteractor.Response> LoginRepository(LoginDto dto);
    }


    public class UserRepository : IUserRepository
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserRepository(
            SignInManager<AppUser> _signInManager,
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager            )
        {
            this.signInManager = _signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IUserLoginInteractor.Response> LoginRepository(LoginDto dto)
        {
            var isLogin = await userManager.FindByNameAsync( dto.UserName );
            if(isLogin != null)
            {   
                return new IUserLoginInteractor.Response("", false);
            }
            return new IUserLoginInteractor.Response("", true);
        }
    }
}
