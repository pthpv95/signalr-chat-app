using Microsoft.AspNetCore.Identity;

namespace chat_service.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}