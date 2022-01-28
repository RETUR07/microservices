using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Entities.Models
{
    public class User : ParentModel
    {
        public List<Chat> Chats { get; set; }
        public string UserId { get; set; }
    }
}
