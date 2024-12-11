using Database.Entities.Identity;
using Models.Common;
using Models.DTOs.Subforums.Input;
using Models.DTOs.Subforums.Output;

namespace Contracts.Services.Entity.Subforums
{
    public interface ISubforumService
    {
        Task<IEnumerable<SubforumListDto>> GetSubforumsForGuestUserAsync(SubforumsQueryDto subforumsQueryDto);

        Task<IEnumerable<SubforumListDto>> GetUserUnjoinedSubforumsAsync(string userId, SubforumsQueryDto subforumsQueryDto);

        Task<IEnumerable<SubforumListDto>> GetUserJoinedSubforumsAsync(string userId, SubforumsQueryDto subforumsQueryDto);
        
        Task<IEnumerable<string>> GetAllSubforumNamesAsync();
     
        Task<IEnumerable<SubforumDropdownDto>> GetSubforumsForDropdownAsync();

        Task<OperationResult<SubforumDetailsDto>> GetSubforumByNameAsync(string name, string userId);

        Task<OperationResult<SubforumDetailsDto>> GetSubforumByNameForGuestUserAsync(string name);

        Task<OperationResult<string>> CreateSubforumAsync(SubforumCreateDto subforumCreateDto, ApplicationUser admin);

        Task<OperationResult> JoinSubforumAsync(long subforumId, ApplicationUser user);

        Task<OperationResult> LeaveSubforumAsync(long subforumId, ApplicationUser user);
    }
}
