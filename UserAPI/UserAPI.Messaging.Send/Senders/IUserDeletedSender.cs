using UserAPI.Messaging.Send.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI.Messaging.Send.Sender
{
    public interface IUserDeletedSender
    {
        void Send(UserForSendDTO dto);
    }
}
