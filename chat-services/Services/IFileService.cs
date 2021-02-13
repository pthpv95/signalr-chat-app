using System;
using System.IO;
using System.Threading.Tasks;
using chat_services.Contracts;
using Microsoft.AspNetCore.Http;
using realtime_app.Contracts;

namespace realtime_app.Services
{
    public interface IFileService
    {
        Task<Guid> StoreFileAsync(IFormFile file);

        Task<DownloadFileContract> StreamFileAsync(Guid id);
    }
}
