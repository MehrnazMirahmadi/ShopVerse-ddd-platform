using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace ShopVerse.IdentityServer.Api.Configuration
{
    public static class ApiScopes
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("shopverse_api", "ShopVerse API")
            };
        }
    }
}
