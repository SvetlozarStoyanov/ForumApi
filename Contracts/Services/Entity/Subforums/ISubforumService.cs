using Database.Entities.Identity;
using Models.Common;
using Models.DTOs.Subforums;

namespace Contracts.Services.Entity.Subforums
{
    public interface ISubforumService
    {
        Task<OperationResult> CreateSubforumAsync(SubforumCreateDto subforumCreateDto, ApplicationUser admin);
    }
}
