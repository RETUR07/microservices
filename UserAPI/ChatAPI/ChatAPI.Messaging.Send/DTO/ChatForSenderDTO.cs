using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Messaging.Send.DTO
{
    public class ChatForSenderDTO
    {
        public int ChatId { get; set; }
        public List<string> Users { get; set; }
    }
}
