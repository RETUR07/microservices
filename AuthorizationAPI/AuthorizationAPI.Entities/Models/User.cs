using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AuthorizationAPI.Entities.Models
{
    public class User : IdentityUser
    {
        public bool IsEnable { get; set; } = true;
    }
}
