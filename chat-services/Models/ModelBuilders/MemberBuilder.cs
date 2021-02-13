using System;
using Microsoft.EntityFrameworkCore;

namespace chatservices.Models.ModelBuilders
{
    public class MemberBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Member>().ToTable("Members");
            entity.HasKey(x => new { x.ConversationId, x.UserId });
            entity.HasOne(x => x.Conversation).WithMany(x => x.Members);
        }
    }
}
