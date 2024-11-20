using Contracts.DataAccess.Repositories.Comments;
using Database;
using Database.Entities.Comments;

namespace DataAccess.Repositories.Comments
{
    public class CommentReplyRepository : RepositoryBase<long, CommentReply>, ICommentReplyRepository
    {
        public CommentReplyRepository(ForumDbContext context) : base(context)
        {
        }
    }
}
