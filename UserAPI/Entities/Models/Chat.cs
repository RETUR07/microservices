using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI.Entities.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public List<User> Users { get; set; }
        public int ChatId { get; set; }
    }
}
