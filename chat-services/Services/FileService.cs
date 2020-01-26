using System;
using System.IO;
using System.Threading.Tasks;
using chat_services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using realtime_app.Contracts;
using realtime_app.Db;
using realtime_app.Models;

namespace realtime_app.Services
{
    public class FileService : IFileService
    {
        private readonly RealtimeAwesomeDbContext _context;

        public FileService(RealtimeAwesomeDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> StoreFileAsync(IFormFile fileInput)
        {
            byte[] content;
            using (var stream = fileInput.OpenReadStream())
            {
                content = ReadAllBytes(stream);
            }
            var file = new FileStorage(fileInput.FileName, fileInput.ContentType, content);
            await _context.Set<FileStorage>().AddAsync(file);
            await _context.SaveChangesAsync();
            return file.Id;
        }

        public byte[] ReadAllBytes(Stream instream)
        {
            if (instream is MemoryStream)
                return ((MemoryStream)instream).ToArray();

            using (var memoryStream = new MemoryStream())
            {
                instream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public async Task<DownloadFileContract> StreamFileAsync(Guid id)
        {
            var file = await _context.Set<FileStorage>().SingleOrDefaultAsync(f => f.Id == id);

            return new DownloadFileContract
            {
                FileName = file.Name,
                ContentType = file.ContentType,
                Content = file.Data
            };
        }
  }
}
