using hotel_management_api.APIs.User.UserDTOs;

namespace hotel_management_api.Business.Interactor.User
{
    public interface IResetPasswordInteractor
    {
    }
    public class ResetPasswordInteractor : IResetPasswordInteractor
    {
        public class Request
        {
            public string? ResetPasswordToken { set; get; }
            public Request(string? resetToken)
            {
                this.ResetPasswordToken = resetToken;    
            }
        }
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public Response(string message, bool success)
            {
                Message = message;
                Success = success;
            }
        }
    }
}
