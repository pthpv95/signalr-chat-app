using Microsoft.AspNetCore.Identity;

namespace realtime_app.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}