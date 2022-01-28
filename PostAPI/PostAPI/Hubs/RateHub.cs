using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PostAPI.Application.Contracts;
using PostAPI.Application.DTO;
using System.Threading.Tasks;

namespace PostAPI.Hubs
{
    [Authorize]
    public class RateHub : Hub
    {
    }
}
