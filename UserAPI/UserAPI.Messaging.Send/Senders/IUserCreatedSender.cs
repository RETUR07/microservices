using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Messaging.Send.DTO;

namespace UserAPI.Messaging.Send.Sender
{
    public interface IUserCreatedSender
    {
        void Send(UserForSendDTO dto);
    }
}
