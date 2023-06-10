using hotel_management_api.Business.Services;

namespace hotel_management_api.Business.Interactor.User
{
    public interface IBlockAndUnlockUserInteractor
    {
        public class Request
        {
            public string token { get; set; }
            public string userId { get; set; }
        }
        public class Response
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
        Task<IBlockAndUnlockUserInteractor.Response> BlockUserAsync(IBlockAndUnlockUserInteractor.Request request);
        Task<IBlockAndUnlockUserInteractor.Response> UnlockUserAsync(IBlockAndUnlockUserInteractor.Request request);
    }
    public class BlockAndUnlockUserInteractor: IBlockAndUnlockUserInteractor
    {
        private readonly IUserService userService;
        public BlockAndUnlockUserInteractor(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IBlockAndUnlockUserInteractor.Response> BlockUserAsync(IBlockAndUnlockUserInteractor.Request request)
        {
            return await userService.BlockUserAsync(request);
        }
        public async Task<IBlockAndUnlockUserInteractor.Response> UnlockUserAsync(IBlockAndUnlockUserInteractor.Request request)
        {
            return await userService.UnlockUserAsync(request);
        }
    }
}
