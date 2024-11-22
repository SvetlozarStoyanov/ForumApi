using Database.Entities.Comments;
using Database.Entities.Identity;
using Database.Entities.Votes;
using Database.Enums.Statuses;

namespace Database.Entities.Posts
{
    public class Post
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public EntityStatus Status { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<PostVote> Votes { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
