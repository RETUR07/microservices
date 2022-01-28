using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthorizationAPI.IdentityTokenServer
{
    internal static class ScopeManager
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("SocialNetwork")
            };
    }
}
