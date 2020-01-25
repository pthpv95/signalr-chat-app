using System;
using System.Collections.Generic;
using realtime_app.Common;

namespace realtime_app.Models
{
    public class FileStorage : AggregateRootBase
    {
        protected FileStorage(){}
        public FileStorage(string name, string type, byte[] data)
        {
            Name = name;
            ContentType = type;
            Data = data;
        }
        public string Name { get; set; }
            
        public string ContentType { get; set; }

        public byte[] Data { get; set; }
    }
}
