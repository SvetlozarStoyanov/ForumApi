using Database.Entities.Identity;
using Models.Common;
using Models.DTOs.Subforums.Input;
using Models.DTOs.Subforums.Output;

namespace Contracts.Services.Entity.Subforums
{
    public interface ISubforumService
    {
        Task<OperationResult<SubforumDetailsDto>> GetSubforumByNameAsync(string name);

        Task<OperationResult> CreateSubforumAsync(SubforumCreateDto subforumCreateDto, ApplicationUser admin);

        Task<OperationResult> JoinSubforumAsync(long subforumId, ApplicationUser user);
    }
}
