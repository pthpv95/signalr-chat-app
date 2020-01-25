namespace chat_services.Contracts
{
    public class DownloadFileContract
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}