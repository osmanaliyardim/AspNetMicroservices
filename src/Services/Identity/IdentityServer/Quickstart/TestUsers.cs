// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityServer4;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users
        {
            get
            {
                var address = new
                {
                    street_address = "Bayrakli Adalet Mh",
                    locality = "Izmir",
                    postal_code = 35390,
                    country = "Turkey"
                };
                
                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "818727",
                        Username = "osman",
                        Password = "o1",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Osman Yardim"),
                            new Claim(JwtClaimTypes.GivenName, "Osman"),
                            new Claim(JwtClaimTypes.FamilyName, "Yardim"),
                            new Claim(JwtClaimTypes.Email, "osmanaliyardim@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://github.com/osmanaliyardim"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json),
                            new Claim(JwtClaimTypes.Role, "admin")
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "88421113",
                        Username = "ali",
                        Password = "a1",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Ali Veli"),
                            new Claim(JwtClaimTypes.GivenName, "Ali"),
                            new Claim(JwtClaimTypes.FamilyName, "Veli"),
                            new Claim(JwtClaimTypes.Email, "AliVeli@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json),
                            new Claim(JwtClaimTypes.Role, "user")
                        }
                    }
                };
            }
        }
    }
}