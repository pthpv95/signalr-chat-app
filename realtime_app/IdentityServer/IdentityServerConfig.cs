using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace realtime_app.IdentityServer
{
    public class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("api.myshop", "MyShop API")
            };
        
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client 
                {
                    ClientId = "vua_spa",
                    ClientName = "Vue SPA",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "openid", "profile", "email", "api.read" },
                    RedirectUris = {"http://localhost:8080/auth-callback"},
                    PostLogoutRedirectUris = {"http://localhost:8080/"},
                    AllowedCorsOrigins = {"http://localhost:8080"},
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 3600
                }
            };
        }
}