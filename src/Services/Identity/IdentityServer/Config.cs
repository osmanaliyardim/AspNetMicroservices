using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "basketClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "basketAPI" }
                },
                new Client
                {
                    ClientId = "uiClient",
                    ClientName = "AspNetRun UI",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:5006/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:5006/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
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
               new IdentityResources.OpenId(),
               new IdentityResources.Profile()
           };

        public static List<TestUser> TestUsers =>
          new List<TestUser>
          {
              new TestUser
              {
                  SubjectId = "5BE8359-073C-434B-AD2D-A3932222DABE",
                  Username = "osman",
                  Password = "osman",
                  Claims = new List<Claim>
                  {
                      new Claim(JwtClaimTypes.GivenName, "osman"),
                      new Claim(JwtClaimTypes.FamilyName, "yardim")
                  }
              }
          };
    }
}
