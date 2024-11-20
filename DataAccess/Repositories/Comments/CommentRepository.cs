using Contracts.DataAccess.Repositories.Comments;
using Database;
using Database.Entities.Comments;

namespace DataAccess.Repositories.Comments
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        public CommentRepository(ForumDbContext context) : base(context)
        {
        }
    }
}
