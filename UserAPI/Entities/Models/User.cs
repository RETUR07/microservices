using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace UserAPI.Entities.Models
{
    public class User : IdentityUser
    {
        public bool IsEnable { get; set; } = true;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public DateTime DateOfBirth { get; set; }


        public List<User> Friends { get; set; }
        public List<User> MakedFriend { get; set; }

        public List<User> Subscribers { get; set; }
        public List<User> Subscribed { get; set; }

        public List<Post> Posts { get; set; }

        public List<Chat> Chats { get; set; }
    }
}
