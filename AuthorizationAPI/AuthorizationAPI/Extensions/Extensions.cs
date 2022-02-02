using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace AuthorizationAPI.Extensions
{
    public static class Extensions
    {
        public static void SetIdentityServerOrigin(this HttpContext context, string value)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (value == null) throw new ArgumentNullException(nameof(value));

            var split = value.Split(new[] { "://" }, StringSplitOptions.RemoveEmptyEntries);

            var request = context.Request;
            request.Scheme = split.First();
            request.Host = new HostString(split.Last());
        }
    }
}
