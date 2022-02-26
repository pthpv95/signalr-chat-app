using System;
using System.IO;
using System.Threading.Tasks;
using chat_services.Contracts;
using Microsoft.AspNetCore.Http;
using chat_service.Contracts;

namespace chat_service.Services
{
    public interface IFileService
    {
        Task<Guid> StoreFileAsync(IFormFile file);

        Task<DownloadFileContract> StreamFileAsync(Guid id);
    }
}
