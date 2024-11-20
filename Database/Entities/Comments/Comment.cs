using Database.Entities.Identity;
using Database.Entities.Posts;
using Database.Entities.Votes;

namespace Database.Entities.Comments
{
    public class Comment
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public long PostId { get; set; }
        public Post Post { get; set; }
        public ICollection<CommentVote> Votes { get; set; }
        public ICollection<CommentReply> Replies { get; set; }
    }
}
