using Database.Entities.Identity;
using Database.Entities.Subforums;
using Models.Common;
using Models.DTOs.Posts;

namespace Contracts.Services.Entity.Posts
{
    public interface IPostService
    {
        Task CreatePostAsync(PostCreateDto postCreateDto,
            ApplicationUser user,
            Subforum subforum);

        Task<OperationResult> UpdatePostAsync(long postId, PostUpdateDto postUpdateDto, string userId);

        Task<OperationResult> DeletePostAsync(long postId, string userId);
    }
}
