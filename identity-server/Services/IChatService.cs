using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public interface IChatService
    {
         Task<string> CreateChatUserAsync(string firstName, string lastName, string userName);
    }
}