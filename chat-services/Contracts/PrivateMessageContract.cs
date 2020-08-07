using System;
using System.Collections.Generic;
using realtime_app.Contracts;

namespace chatservices.Contracts
{
    public class PrivateMessageContract
    {
        public List<string> ConnectionIds { get; set; }

        public PrivateMessageActionType ActionType { get; set; }

        public SendMessageRequestContract NewMessage { get; set; }

        public MessageHasSeenReponseContract SeenMessage { get; set; }
    }

    public enum PrivateMessageActionType
    {
        NewMessage,
        SeenMessage
    }
}
