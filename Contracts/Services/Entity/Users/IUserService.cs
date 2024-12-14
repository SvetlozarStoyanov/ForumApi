using Models.Common;
using Models.DTOs.Users.Output;

namespace Contracts.Services.Entity.Users
{
    public interface IUserService
    {
        Task<bool> IsUserNameTakenAsync(string userName);

        Task<IEnumerable<string>> GetAllUsernamesAsync();

        Task<IEnumerable<UserSearchDto>> SearchUsersAsync(string searchTerm);

        Task<OperationResult<UserDetailsDto>> GetUserDetailsAsync(string userName);

        Task<OperationResult<UserShortInfoDto>> GetUserByIdAsync(string userId);
    }
}
