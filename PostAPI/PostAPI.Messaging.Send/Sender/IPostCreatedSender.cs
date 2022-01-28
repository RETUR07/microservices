using PostAPI.Messaging.Send.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostAPI.Messaging.Send.Sender
{
    public interface IPostCreatedSender
    {
        void Send(PostForSender dto);
    }
}
