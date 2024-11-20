using Database.Entities.Identity;

namespace Database.Entities.Comments
{
    public class CommentReply
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public long CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
