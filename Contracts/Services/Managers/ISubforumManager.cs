using Models.Common;
using Models.DTOs.Subforums;

namespace Contracts.Services.Managers
{
    public interface ISubforumManager
    {
        Task<OperationResult> CreateSubforumAsync(SubforumCreateDto subforumCreateDto, string userId);
    }
}
