using System.Threading.Tasks;

namespace realtime_app.Services
{
    public interface IMessageService
    {
         Task CreateMessageAsync(string message, int userId, int conversationId);
    }
}