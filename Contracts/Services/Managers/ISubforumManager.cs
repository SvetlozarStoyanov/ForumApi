using Models.Common;
using Models.DTOs.Subforums.Input;
using Models.DTOs.Subforums.Output;

namespace Contracts.Services.Managers
{
    public interface ISubforumManager
    {
        Task<OperationResult<SubforumDetailsDto>> GetSubforumByNameAsync(string name);

        Task<OperationResult> CreateSubforumAsync(string userId, SubforumCreateDto subforumCreateDto);

        Task<OperationResult> JoinSubforumAsync(long subforumId, string userId);
    }
}
