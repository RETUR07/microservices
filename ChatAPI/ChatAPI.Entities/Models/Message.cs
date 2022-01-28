using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAPI.Entities.Models
{
    public class Message : ParentModel
    {
        public string UserId { get; set; }
        public string Text { get; set; }
        public int FileCount { get; set; }
        public List<Blob> Blobs { get; set; }
        public Chat Chat { get; set; }
    }
}
