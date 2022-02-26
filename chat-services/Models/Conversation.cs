using System;
using System.Collections.Generic;
using System.Linq;
using chatservices.Models;
using chat_service.Common;

namespace chat_service.Models
{
    public class Conversation : AggregateRootBase
    {
        protected Conversation() { }

        public Conversation(string title, Guid creatorId, Guid memberId)
        {
            Title = title;
            CreatorId = creatorId;
            AddMembers(new List<Guid> { creatorId, memberId });
            Messages = new List<Message>();
        }

        public string Title { get; set; }

        public Guid CreatorId { get; set; }

        public User Creator { get; set; }

        public ICollection<Member> Members { get; set; }

        public ICollection<Message> Messages { get; set; }

        public void AddMembers(IList<Guid> userIds)
        {
            if (Members == null || Members.Count == 0)
            {
                Members = userIds.Select(userId => new Member(Id, userId)).ToList();
            }
            else
            {
                foreach (var member in Members)
                {
                    if (!Members.Any(m => m.Equals(member)))
                    {
                        Members.Add(new Member(Id, member.UserId));
                    }
                }
            }
        }
    }
}