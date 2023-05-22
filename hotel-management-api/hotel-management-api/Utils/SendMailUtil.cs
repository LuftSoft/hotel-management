using MailKit.Net.Smtp;
using MimeKit;

namespace hotel_management_api.Utils
{
    public interface ISendMailUtil
    {
        public class Response
        {
            public string? Message { get; set; }
            public bool? Success { get; set; }
            public Response(string? message, bool? success)
            {
                Message = message;
                Success = success;
            }
        }
        Task<ISendMailUtil.Response> SendMailAsync(string? to, string? subject, string? content);
        
    }
    public class SendMailUtil : ISendMailUtil
    {
        private readonly IConfiguration configuration;
        public SendMailUtil(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<ISendMailUtil.Response> SendMailAsync(string? to, string? subject, string? content)
        {
            var htmlBody = new BodyBuilder();
            htmlBody.HtmlBody = content;
            var email = new MimeMessage();
            email.To.Add(MailboxAddress.Parse(to));
            email.Sender = new MailboxAddress(configuration.GetSection("SendMailService:DisplayName").Value, configuration.GetSection("SendMailService:Email").Value);
            email.From.Add(new MailboxAddress(configuration.GetSection("SendMailService:DisplayName").Value, configuration.GetSection("SendMailService:Email").Value));
            email.Subject = subject; 
            email.Body = htmlBody.ToMessageBody();
            using var smtpClient = new SmtpClient();
            try
            {
                smtpClient.Connect(configuration.GetSection("SendMailService:Host").Value, Int32.Parse(configuration.GetSection("SendMailService:Port").Value), MailKit.Security.SecureSocketOptions.StartTls);
                smtpClient.Authenticate(configuration.GetSection("SendMailService:Email").Value, configuration.GetSection("SendMailService:Password").Value);
                var result = await smtpClient.SendAsync(email);

                smtpClient.Disconnect(true);
                return new ISendMailUtil.Response("send mail success", true);
            }
            catch(Exception ex)
            {
                smtpClient.Disconnect(true);
                return new ISendMailUtil.Response(ex.Message, false);
            }
        }
    }
}
