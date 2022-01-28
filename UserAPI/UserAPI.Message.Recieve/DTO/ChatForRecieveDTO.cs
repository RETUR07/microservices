using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI.Messaging.Recieve.DTO
{
    public class ChatForRecieveDTO
    {
        public int ChatId { get; set; }
        public List<string> Users { get; set; }
    }
}
