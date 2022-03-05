// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { };

        public static IEnumerable<Client> Clients =>
            new Client[] 
            { 
                new Client()
                {
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = new List<string>()
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile
                        },
                    ClientName = "BlazorServerClient",
                    ClientId = "blazorserverclient",
                    ClientSecrets = new List<Secret>()
                        {
                            new Secret("secret".Sha256())
                        },
                    RedirectUris = new List<string>()
                        {
                            "http://localhost:5050/signin-oidc"
                        }

                }};
    }
}