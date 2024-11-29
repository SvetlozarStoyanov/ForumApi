using Database.Entities.Identity;
using Database.Entities.Posts;
using Database.Enums.Votes;

namespace Contracts.Services.Entity.Votes
{
    public interface IPostVoteService
    {
        void VoteOnPost(PostVotes type, Post post, ApplicationUser user);
    }
}
