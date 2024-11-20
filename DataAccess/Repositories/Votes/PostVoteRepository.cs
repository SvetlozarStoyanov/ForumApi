using Contracts.DataAccess.Repositories.Votes;
using Database;
using Database.Entities.Votes;

namespace DataAccess.Repositories.Votes
{
    public class PostVoteRepository : RepositoryBase<long, PostVote>, IPostVoteRepository
    {
        public PostVoteRepository(ForumDbContext context) : base(context)
        {
        }
    }
}
