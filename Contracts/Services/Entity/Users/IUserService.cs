using Models.Common;
using Models.DTOs.Users.Output;

namespace Contracts.Services.Entity.Users
{
    public interface IUserService
    {
        Task<bool> IsUserNameTakenAsync(string userName);

        Task<OperationResult<UserShortInfoDto>> GetUserByIdAsync(string userId);
    }
}
