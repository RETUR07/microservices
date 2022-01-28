using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI.Entities.Models
{
    public class Post
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
    }
}
