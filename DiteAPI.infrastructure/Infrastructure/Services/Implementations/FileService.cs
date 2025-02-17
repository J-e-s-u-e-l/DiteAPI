using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.Infrastructure.Infrastructure.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _uploadPath;
        private readonly ILogger<FileService> _logger;


        public FileService(IWebHostEnvironment webHostEnvironment, ILogger<FileService> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _uploadPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadedResources");
            _logger = logger;
        }

        //private readonly string _uploadPath = Path.Combine(, "UploadedResources");
        public async Task<string> SaveFileAsync(IFormFile file, Guid academyId)
        {
            var academyFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadedAcademyResources", academyId.ToString());

            Directory.CreateDirectory(academyFolder);

            var filePath = Path.Combine(academyFolder, Guid.NewGuid() + Path.GetExtension(file.FileName));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
        /*
                public async Task<FileStream> GetFileAsync(string filePath)
                {
                    if (!File.Exists(filePath))
                    {
                        throw new FileNotFoundException("File not found", filePath);
                    }

                    return new FileStream(filePath, FileMode.Open, FileAccess.Read);
                }*/
        public async Task<MemoryStream> GetFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                MemoryStream memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                return memoryStream;
            }
        }

        public void DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        stream.Close();
                    }

                    FileInfo fileInfo = new FileInfo(filePath);

                    File.Delete(filePath);
                }
            }
            catch (UnauthorizedAccessException)
            {
                FileAttributes attributes = File.GetAttributes(filePath);
                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    attributes &= ~FileAttributes.ReadOnly;
                    File.SetAttributes(filePath, attributes);
                    File.Delete(filePath);
                }
                else
                {
                    throw;
                }
            }
        }


        public string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}