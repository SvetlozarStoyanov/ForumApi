using Contracts.DataAccess.Repositories.Posts;
using Database;
using Database.Entities.Posts;

namespace DataAccess.Repositories.Posts
{
    public class PostRepository : RepositoryBase<long, Post>, IPostRepository
    {
        public PostRepository(ForumDbContext context) : base(context)
        {
        }
    }
}
