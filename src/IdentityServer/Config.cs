using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { new ApiScope("api1", "My API") };

        public static IEnumerable<Client> Clients =>
        new Client[] 
        { 
            new Client
            {
                ClientId = "client1",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret1".Sha256())
                },
                AllowedScopes = { "api1" }
            } 
        };
    }
}