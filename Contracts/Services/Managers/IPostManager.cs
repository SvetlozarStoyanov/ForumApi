using Models.Common;
using Models.DTOs.Posts;

namespace Contracts.Services.Managers
{
    public interface IPostManager
    {
        Task<OperationResult> CreatePostAsync(PostCreateDto postCreateDto, string userId);

        Task<OperationResult> UpdatePostAsync(long postId, PostUpdateDto postUpdateDto, string userId);

        Task<OperationResult> DeletePostAsync(long postId, string userId);
    }
}
