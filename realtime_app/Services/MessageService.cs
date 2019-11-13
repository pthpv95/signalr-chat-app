using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using realtime_app.Db;
using realtime_app.Models;

namespace realtime_app.Services
{
  public class MessageService : IMessageService
  {
    private RealtimeAwesomeDbContext _context;
    public MessageService(RealtimeAwesomeDbContext context)
    {
      _context = context;
    }

    public async Task CreateMessageAsync(string message, int senderId, int conversationId)
    {
      // var conversation = _context.Set<Conversation>().SingleOrDefaultAsync(c => c.CreatorId == senderId);
      var newMessage = new Message(message, senderId, 0, conversationId);
      await _context.Set<Message>().AddAsync(newMessage);
      await _context.SaveChangesAsync();
    }
  }
}