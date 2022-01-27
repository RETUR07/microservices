using ChatAPI.Messaging.Send.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Messaging.Send.Sender
{
    public interface IChatDeletedSender
    {
        void Send(ChatForSenderDTO dto);
    }
}
