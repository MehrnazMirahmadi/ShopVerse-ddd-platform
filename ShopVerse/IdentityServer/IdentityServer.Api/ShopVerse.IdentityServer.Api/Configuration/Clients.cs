using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace ShopVerse.IdentityServer.Api.Configuration
{
    public static class Clients
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "shopverse_web",
                    ClientName = "ShopVerse Web Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false, // set true if you want secret for server-side clients
                    RedirectUris = { "https://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "shopverse_api"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
        }
    }
}
