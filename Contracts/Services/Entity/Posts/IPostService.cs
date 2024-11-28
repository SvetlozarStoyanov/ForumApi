using Database.Entities.Identity;
using Database.Entities.Subforums;
using Models.Common;
using Models.DTOs.Posts.Input;
using Models.DTOs.Posts.Output;

namespace Contracts.Services.Entity.Posts
{
    public interface IPostService
    {
        Task<IEnumerable<PostListDto>> GetHomePagePostsForGuestUserAsync();

        Task<IEnumerable<PostListDto>> GetSubforumPostsAsync(long subforumId);

        Task<OperationResult<PostDetailsDto>> GetPostDetailsByIdAsync(long id);

        Task CreatePostAsync(PostCreateDto postCreateDto,
            ApplicationUser user,
            Subforum subforum);

        Task<OperationResult> UpdatePostAsync(long postId, string userId, PostUpdateDto postUpdateDto);

        Task<OperationResult> DeletePostAsync(long postId, string userId);
    }
}
