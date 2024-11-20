using DataAccess.Repositories;
using Database;
using Database.Entities.Votes;

namespace Contracts.DataAccess.Repositories.Votes
{
    public class CommentVoteRepository : RepositoryBase<long, CommentVote>, ICommentVoteRepository
    {
        public CommentVoteRepository(ForumDbContext context) : base(context)
        {
        }
    }
}
