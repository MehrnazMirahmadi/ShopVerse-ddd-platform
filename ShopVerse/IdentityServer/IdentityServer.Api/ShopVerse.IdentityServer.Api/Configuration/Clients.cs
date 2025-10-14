using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace ShopVerse.IdentityServer.Api.Configuration
{
    public static class Clients
    {
        public static IEnumerable<Client> GetClients(IConfiguration configuration) =>
            new List<Client>
            {
            new Client
            {
                ClientId = "inventory-client",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "inventory-api" }
            }
            };
    }

}

