using Database.Entities.Votes;

namespace Contracts.DataAccess.Repositories.Votes
{
    public interface ICommentVoteRepository : IRepositoryBase<long, CommentVote>
    {
    }
}
