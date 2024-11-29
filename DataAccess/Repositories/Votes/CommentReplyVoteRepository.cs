using Contracts.DataAccess.Repositories.Votes;
using Database;
using Database.Entities.Votes;

namespace DataAccess.Repositories.Votes
{
    public class CommentReplyVoteRepository : RepositoryBase<long, CommentReplyVote>, ICommentReplyVoteRepository
    {
        public CommentReplyVoteRepository(ForumDbContext context) : base(context)
        {
        }
    }
}
