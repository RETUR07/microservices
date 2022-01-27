using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using ChatAPI.Entities.Models;
using System.Linq;
using System.Security.Claims;

namespace ChatAPI.Hubs
{
    [Authorize]
    public class BaseHub : Hub
    {
        public string UserId
        {
            get
            {
                return Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            }
        }
    }
}
