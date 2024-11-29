using Database.Entities.Comments;
using Database.Entities.Identity;
using Database.Enums.Votes;

namespace Contracts.Services.Entity.Votes
{
    public interface ICommentReplyVoteService
    {
        void VoteOnCommentReply(CommentReplyVotes type, CommentReply commentReply, ApplicationUser user);
    }
}
