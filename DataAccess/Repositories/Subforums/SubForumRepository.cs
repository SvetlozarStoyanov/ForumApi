using Contracts.DataAccess.Repositories.Subforums;
using Database;
using Database.Entities.Subforums;

namespace DataAccess.Repositories.Subforums
{
    public class SubForumRepository : RepositoryBase<long, Subforum>, ISubForumRepository
    {
        public SubForumRepository(ForumDbContext context) : base(context)
        {
        }
    }
}
