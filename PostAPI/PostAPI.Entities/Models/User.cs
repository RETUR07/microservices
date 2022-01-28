using System.Collections.Generic;

namespace PostAPI.Entities.Models
{
    public class User : ParentModel
    {
        public string UserId { get; set; }
        public List<Post> Posts { get; set; }
        public List<Rate> Rates { get; set; }
    }
}
