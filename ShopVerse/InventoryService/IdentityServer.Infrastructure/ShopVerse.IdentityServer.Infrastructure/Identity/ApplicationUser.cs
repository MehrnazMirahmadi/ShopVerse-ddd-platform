using Microsoft.AspNetCore.Identity;

namespace ShopVerse.IdentityServer.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
