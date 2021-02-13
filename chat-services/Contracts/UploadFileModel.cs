using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace realtime_app.Contracts
{
    public class UploadFileModel
    {
        public string FileName { get; set; }

        public int ContentType { get; set; }

        public IFormFile File { get; set; }
    }
}
