// ApiResources.cs
using Duende.IdentityServer.Models;

namespace ShopVerse.IdentityServer.Api.Configuration;

public static class ApiResources
{
    public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>
        {
           new ApiResource("inventory-api", "Inventory API")
            {
                Scopes = { "inventory-api" },
                ApiSecrets = { new Secret("secret".Sha256()) }
            }
        };
}
