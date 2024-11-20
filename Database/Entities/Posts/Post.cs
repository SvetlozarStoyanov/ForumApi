using Database.Entities.Comments;
using Database.Entities.Identity;
using Database.Entities.Votes;

namespace Database.Entities.Posts
{
    public class Post
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public ICollection<PostVote> Votes { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
