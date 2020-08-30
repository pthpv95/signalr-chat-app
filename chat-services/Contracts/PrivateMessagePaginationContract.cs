using System;

namespace chat_services.Contracts
{
    public class PrivateMessagePaginationContract
    {
        public Guid UserId { get; set; }

        public Guid ContactUserId { get; set; }

        public Guid? ConversationId { get; set; }

        public DateTime? Cursor { get; set; }

        public int PageSize { get; set; } = 10;
    }
}