using Contracts.DataAccess.Repositories.Users;
using Database;
using Database.Entities.Identity;

namespace DataAccess.Repositories.Users
{
    public class UserRepository : RepositoryBase<string, ApplicationUser>, IUserRepository
    {
        public UserRepository(ForumDbContext context) : base(context)
        {
        }
    }
}
