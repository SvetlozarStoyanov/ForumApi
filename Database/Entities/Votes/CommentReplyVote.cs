using Database.Entities.Comments;
using Database.Enums.Votes;

namespace Database.Entities.Votes
{
    public class CommentReplyVote
    {
        public long Id { get; set; }
        public long CommentReplyId { get; set; }
        public CommentReply CommentReply { get; set; }
        public CommentReplyVotes Type { get; set; }
    }
}
