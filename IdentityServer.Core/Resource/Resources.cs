using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer.Core.Resource
{
    public class Resources
    {
        /* Setting the enable API info.
         */
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("DevApi", "DEV Api", new List<string>{ JwtClaimTypes.Role }),
                new ApiResource("UatApi", "UAT Api", new List<string>{ JwtClaimTypes.Role }),
            };
        }
                
        /* Setting API range for the client.
         */
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("DevApi", "DEV Api"){ 
                    UserClaims = {"role" }
                },
                new ApiScope("UatApi", "UAT Api"),
            };
        }

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
    }
}
