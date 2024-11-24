using Models.Common;
using Models.DTOs.Subforums;

namespace Contracts.Services.Managers
{
    public interface ISubforumManager
    {
        Task<OperationResult> CreateSubforumAsync(string userId, SubforumCreateDto subforumCreateDto);

        Task<OperationResult> JoinSubforumAsync(long subforumId, string userId);
    }
}
