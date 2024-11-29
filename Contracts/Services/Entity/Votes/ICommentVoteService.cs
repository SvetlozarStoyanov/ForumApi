using Database.Entities.Comments;
using Database.Entities.Identity;
using Database.Enums.Votes;

namespace Contracts.Services.Entity.Votes
{
    public interface ICommentVoteService
    {
        void VoteOnComment(CommentVotes type, Comment comment, ApplicationUser user);
    }
}
