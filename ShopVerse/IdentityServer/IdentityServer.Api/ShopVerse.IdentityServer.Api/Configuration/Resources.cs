using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace ShopVerse.IdentityServer.Api.Configuration
{
    public static class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),   // required
                new IdentityResources.Profile(),  // includes standard profile info like name, email
                // Add more identity resources if needed
            };
        }
    }
}
