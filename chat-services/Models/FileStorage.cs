using System;
using System.Collections.Generic;
using chat_service.Common;

namespace chat_service.Models
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
