using Database.Entities.Identity;
using Database.Entities.Votes;
using Database.Enums.Statuses;

namespace Database.Entities.Comments
{
    public class CommentReply
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public EntityStatus Status { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public long CommentId { get; set; }
        public Comment Comment { get; set; }
        public ICollection<CommentVote> Votes { get; set; }
    }
}
