using Database.Entities.Identity;

namespace Contracts.DataAccess.Repositories.Users
{
    public interface IUserRepository : IRepositoryBase<string, ApplicationUser>
    {
    }
}
