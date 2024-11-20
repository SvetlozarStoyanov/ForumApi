using Database.Entities.Identity;
using Database.Entities.Posts;
using Database.Enums.Votes;

namespace Database.Entities.Votes
{
    public class PostVote
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public long PostId { get; set; }
        public Post Post { get; set; }
        public PostVotes Type { get; set; }
    }
}
