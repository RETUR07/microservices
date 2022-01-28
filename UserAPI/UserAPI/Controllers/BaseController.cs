using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Entities.Models;
using System.Linq;
using System.Security.Claims;

namespace UserAPI.Controllers
{
    [ApiController]
    public class Base : ControllerBase
    {
        public string UserId
        {
            get
            {
                var claim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                return claim.Value;
            }
        }     
    }
}
