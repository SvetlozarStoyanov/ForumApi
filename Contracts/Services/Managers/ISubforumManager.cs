using Models.Common;
using Models.DTOs.Subforums.Input;
using Models.DTOs.Subforums.Output;

namespace Contracts.Services.Managers
{
    public interface ISubforumManager
    {
        Task<IEnumerable<SubforumListDto>> GetSubforumsForGuestUserAsync(SubforumsQueryDto subforumsQueryDto);
        
        Task<OperationResult<IEnumerable<SubforumListDto>>> GetUserUnjoinedSubforumsAsync(string userId, SubforumsQueryDto subforumsQueryDto);

        Task<OperationResult<IEnumerable<SubforumListDto>>> GetUserJoinedSubforumsAsync(string userId, SubforumsQueryDto subforumsQueryDto);

        Task<IEnumerable<string>> GetAllSubforumNamesAsync();

        Task<IEnumerable<SubforumDropdownDto>> GetSubforumsForDropdownAsync();

        Task<OperationResult<SubforumDetailsDto>> GetSubforumByNameAsync(string name, string? userId);

        Task<OperationResult<string>> CreateSubforumAsync(string userId, SubforumCreateDto subforumCreateDto);

        Task<OperationResult> JoinSubforumAsync(long subforumId, string userId);

        Task<OperationResult> LeaveSubforumAsync(long subforumId, string userId);
    }
}
