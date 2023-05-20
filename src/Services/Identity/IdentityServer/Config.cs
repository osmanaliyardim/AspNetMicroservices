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
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:8006/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:8006/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        "basketAPI",
                        "role"
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("basketAPI", "Basket API")
           };

        public static IEnumerable<IdentityResource> IdentityResources =>
           new IdentityResource[]
           {
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),
               new IdentityResources.Address(),
               new IdentityResources.Email(),
               new IdentityResource(
                   "role",
                   "Role",
                   new List<string>() { "role" })
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
