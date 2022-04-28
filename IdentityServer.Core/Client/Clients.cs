using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using client = IdentityServer4.Models.Client;

namespace IdentityServer.Core.Client
{
    public class Clients
    {
        public static IEnumerable<client> GetClients()
        {
            return new List<client>
            {
                new client
                {
                    ClientId = "Admin",
                    // Customer authorization model
                    AllowedGrantTypes = GrantTypes.ClientCredentials,                    
                    /* If the ApiScope value is different, the token result will show
                     * "Invalid scope" error messages.
                     */
                    AllowedScopes = { "DevApi", "UatApi" },
                    ClientSecrets = { new Secret("adminSecret".Sha256())},
                    // Admin must have the two identities.
                    Claims = new List<ClientClaim>
                    {
                        new ClientClaim(JwtClaimTypes.Role, "admin"),
                        new ClientClaim(JwtClaimTypes.Role, "user")
                    },
                    /* If the code without the ClientClaimsPrefix field, the token will return the client_role fields,
                     * which is not "role" field. When the return field can't map the role, the valiated result will be false.
                     */
                    ClientClaimsPrefix = string.Empty
                },
                new client
                {
                    ClientId = "User",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "UatApi" },
                    ClientSecrets = { new Secret("userSecret".Sha256())},
                    Claims = new List<ClientClaim>
                    {
                        new ClientClaim(JwtClaimTypes.Role, "user")
                    },
                    ClientClaimsPrefix = string.Empty
                },
                new client
                {
                    ClientId = "Dev-User",
                    ClientName = "Test client (Code with PKCE)",

                    AllowedGrantTypes = GrantTypes.Code,

                    AllowedScopes = {
                        "DevApi",
                        "UatApi",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },
                    AllowedCorsOrigins = { "https://localhost:44328" },
                    RedirectUris = new [] {"http://localhost:55836/swagger/oauth2-redirect.html" },
                    
                    ClientSecrets = { new Secret("dev_Secret".Sha256())},
                    Claims = new List<ClientClaim>
                    {
                        new ClientClaim(JwtClaimTypes.Role, "admin"),
                        new ClientClaim(JwtClaimTypes.Role, "user")
                    },
                    ClientClaimsPrefix = string.Empty,
                    RequirePkce = true
                }
            };
        }

        public static List<TestUser> TestUsers => new()
        { 
            new TestUser {
                SubjectId = "1",
                Username = "admin",
                Password = "123456",
                Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
            }
        };
    }
}
