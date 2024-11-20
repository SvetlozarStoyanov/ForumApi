using Database.Entities.Comments;

namespace Contracts.DataAccess.Repositories.Comments
{
    public interface ICommentRepository : IRepositoryBase<long, Comment>
    {
    }
}
