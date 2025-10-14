// ApiScopes.cs
using Duende.IdentityServer.Models;

namespace ShopVerse.IdentityServer.Api.Configuration;

public static class ApiScopes
{
    public static IEnumerable<ApiScope> GetApiScopes() =>
        new List<ApiScope>
        {
            new ApiScope("inventory-api", "Inventory API")
        };
}
