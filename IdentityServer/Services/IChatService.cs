using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public interface IChatService
    {
         Task<int> CreateChatUserAsync(string firstName, string lastName, string userName);
    }
}