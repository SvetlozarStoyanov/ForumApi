using Database.Entities.Posts;

namespace Contracts.DataAccess.Repositories.Posts
{
    public interface IPostRepository : IRepositoryBase<long, Post>
    {
    }
}
