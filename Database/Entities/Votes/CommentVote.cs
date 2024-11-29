using Database.Entities.Comments;
using Database.Entities.Identity;
using Database.Enums.Votes;

namespace Database.Entities.Votes
{
    public class CommentVote
    {
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public CommentVotes Type { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public long CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
