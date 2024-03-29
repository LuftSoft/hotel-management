﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace hotel_management_api.Utils
{
    public interface IUploadFileUtil
    {
        Task<string?> UploadAsync(IFormFile uploadFile);
        Task<IEnumerable<string>> MultiUploadAsync(IFormFile[] files);
    }
    public class UploadFileUtil : IUploadFileUtil
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;
        public UploadFileUtil
            (
            IConfiguration configuration
            , IWebHostEnvironment environment
            )
        {
            this.configuration = configuration;
            this.environment = environment;
        }
        public async Task<string?> UploadAsync(IFormFile uploadFile)
        {
            try
            {
                var cloudinary = new Cloudinary(this.configuration["CLOUDINARY_URL"].ToString());
                cloudinary.Api.Secure = true;
                var basePath = Path.Combine(environment.WebRootPath, "upload");
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
        public async Task<IEnumerable<string>> MultiUploadAsync(IFormFile[] files)
        {
            try
            {
                var cloudinary = new Cloudinary(this.configuration["CLOUDINARY_URL"].ToString());
                cloudinary.Api.Secure = true;
                var basePath = Path.Combine(environment.WebRootPath, "upload");
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                var result = new List<string>();
                foreach(var file in files)
                {
                    var filepath = basePath + @$"/image-upload-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.jpg";
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var uploadParam = new ImageUploadParams()
                    {
                        File = new FileDescription(filepath),
                        UseFilename = true,
                        Folder = "hotel_management",
                        UniqueFilename = true,
                        Overwrite = true
                    };
                    var uploadResult = await cloudinary.UploadAsync(uploadParam);
                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);
                    }
                    result.Add(uploadResult.Url.ToString());
                }
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
