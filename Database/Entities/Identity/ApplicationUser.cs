using Database.Entities.Comments;
using Database.Entities.Posts;
using Database.Entities.Subforums;
using Microsoft.AspNetCore.Identity;

namespace Database.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Subforum> Subforums { get; set; }
        public ICollection<Subforum> AdministratedSubforums { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<CommentReply> CommentReplies { get; set; }
    }
}
