using Database.Entities.Identity;
using Models.Common;
using Models.DTOs.Subforums.Input;
using Models.DTOs.Subforums.Output;

namespace Contracts.Services.Entity.Subforums
{
    public interface ISubforumService
    {
        Task<IEnumerable<string>> GetAllSubforumNamesAsync();
     
        Task<IEnumerable<SubforumDropdownDto>> GetSubforumsForDropdownAsync();

        Task<OperationResult<SubforumDetailsDto>> GetSubforumByNameAsync(string name);

        Task<OperationResult<string>> CreateSubforumAsync(SubforumCreateDto subforumCreateDto, ApplicationUser admin);

        Task<OperationResult> JoinSubforumAsync(long subforumId, ApplicationUser user);
    }
}
