using Database.Entities.Votes;

namespace Contracts.DataAccess.Repositories.Votes
{
    public interface IPostVoteRepository : IRepositoryBase<long, PostVote>
    {
    }
}
