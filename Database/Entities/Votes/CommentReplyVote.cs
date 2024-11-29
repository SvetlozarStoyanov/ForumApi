using Database.Entities.Comments;
using Database.Entities.Identity;
using Database.Enums.Votes;

namespace Database.Entities.Votes
{
    public class CommentReplyVote
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public CommentReplyVotes Type { get; set; }
        public ApplicationUser User { get; set; }
        public long CommentReplyId { get; set; }
        public CommentReply CommentReply { get; set; }
    }
}
