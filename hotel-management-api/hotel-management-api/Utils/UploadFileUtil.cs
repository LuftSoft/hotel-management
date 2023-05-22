using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace hotel_management_api.Utils
{
    public interface IUploadFileUtil
    {
        Task<string?> Upload(IFormFile uploadFile);
    }
    public class UploadFileUtil : IUploadFileUtil
    {
        private readonly IConfiguration configuration;
        public UploadFileUtil(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string?> Upload(IFormFile uploadFile)
        {
            try
            {
                var cloudinary = new Cloudinary(this.configuration["CLOUDINARY_URL"].ToString());
                cloudinary.Api.Secure = true;
                var basePath = Directory.GetCurrentDirectory() + @"wwwroot/upload";
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                var filepath = basePath + @$"/fileupload-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.jpg";
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(stream);
                }
                var uploadParam = new ImageUploadParams()
                {
                    File = new FileDescription(filepath),
                    UseFilename = true,
                    Folder = "hotel_management",
                    UniqueFilename = true,
                    Overwrite = true
                };
                var result = await cloudinary.UploadAsync(uploadParam);
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                return result.Url.ToString();
            }
            catch
            {
                return null;
            }
            }
    }
}
