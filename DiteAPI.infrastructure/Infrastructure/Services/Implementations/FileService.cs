using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
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

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _uploadPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadedResources");
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

        public async Task<FileStream> GetFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }

        /*public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }*/
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

                    /*// Ensure full permissions
                    FileSecurity fileSecurity = fileInfo.GetAccessControl();
                    fileSecurity.SetAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                    fileInfo.SetAccessControl(fileSecurity);*/

                    // Remove read-only attribute if set
                    if(fileInfo.IsReadOnly)
                    {
                        fileInfo.IsReadOnly = false;
                    }

                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                throw;
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