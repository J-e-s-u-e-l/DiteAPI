using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.Infrastructure.Infrastructure.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, Guid academyId);
        //Task<FileStream> GetFileAsync(string filePath);
        Task<MemoryStream> GetFileAsync(string filePath);
        void DeleteFile(string filePath);
        string GetContentType(string path);
    }
}
