using System.ComponentModel.DataAnnotations.Schema;

namespace PostAPI.Entities.Models
{
    public class Rate : ParentModel
    {
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

        public LikeStatus LikeStatus { get; set; }
    }
}
