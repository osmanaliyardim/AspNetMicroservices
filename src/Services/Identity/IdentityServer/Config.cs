using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "aspnetrunClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "basketAPI" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("basketAPI", "Basket API")
           };

        public static IEnumerable<ApiResource> ApiResources =>
           new ApiResource[]
           {

           };

        public static IEnumerable<IdentityResource> IdentityResources =>
           new IdentityResource[]
           {

           };

        public static List<TestUser> TestUsers =>
          new List<TestUser>
          {

          };
    }
}
