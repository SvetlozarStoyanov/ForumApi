using Database.Entities.Comments;

namespace Contracts.DataAccess.Repositories.Comments
{
    public interface ICommentReplyRepository : IRepositoryBase<long, CommentReply>
    {
    }
}
