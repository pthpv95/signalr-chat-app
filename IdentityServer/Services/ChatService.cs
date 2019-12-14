using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using IdentityServer.Models.AccountViewModels;

namespace IdentityServer.Services
{
    public class ChatService : IChatService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        
        public ChatService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<int> CreateChatUserAsync(string firstName, string lastName, string userName)
        {
            var client = _httpClientFactory.CreateClient("ChatApp");

            var content = new StringContent(JsonConvert.SerializeObject(
                new {
                    firstName,
                    lastName,
                    userName
                }), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress + "api/users")
            {
                Content = content
            };

            var response = await client.SendAsync(request);

            var result = await response.Content.ReadAsAsync<CreateChatUserResponse>();
            return result.Id;
        }
    }
}