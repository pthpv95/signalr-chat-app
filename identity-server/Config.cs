// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServerWithAspNetIdentity
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SinglePage",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AccessTokenLifetime = 3600,
                    RedirectUris = {
                        "http://localhost:8080/callback",
                        "http://awesome-chat-images.s3-website-ap-southeast-1.amazonaws.com/silent-renew",
                        "http://localhost:8080/callback",
                        "http://awesome-chat-images.s3-website-ap-southeast-1.amazonaws.com/silent-renew",
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    PostLogoutRedirectUris = 
                    {
                        "http://localhost:5001/account/login",
                        "http://159.65.14.138:5001/account/login"
                    },
                    AllowedCorsOrigins = {
                        "https://localhost:8080/", "http://localhost:8080/",
                        "http://awesome-chat-images.s3-website-ap-southeast-1.amazonaws.com/", "https://awesome-chat-images.s3-website-ap-southeast-1.amazonaws.com/"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                }
            };
        }
    }
}