using ChatAPI.Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChatAPI.Entities.Models
{
    public class Chat : ParentModel
    {
        public List<User> UserIds { get; set; }
        public List<Message> Messages { get; set; }
    }
}
