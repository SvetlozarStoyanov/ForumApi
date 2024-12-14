using Database.Entities.Identity;
using Database.Entities.Posts;
using Database.Entities.Subforums;
using Models.Common;
using Models.DTOs.Posts.Input;
using Models.DTOs.Posts.Output;

namespace Contracts.Services.Entity.Posts
{
    public interface IPostService
    {
        Task<IEnumerable<PostListDto>> GetUserPostsForGuestUserAsync(string userId, PostsQueryDto postsQueryDto);

        Task<IEnumerable<PostListDto>> GetUserPostsAsync(string userId, string currentUserId, PostsQueryDto postsQueryDto);

        Task<IEnumerable<PostListDto>> GetHomePagePostsForGuestUserAsync(PostsQueryDto postsQueryDto);

        Task<IEnumerable<PostListDto>> GetHomePagePostsAsync(string userId, PostsQueryDto postsQueryDto);

        Task<IEnumerable<PostListDto>> GetSubforumPostsForGuestUserAsync(long subforumId, PostsQueryDto postsQueryDto);

        Task<IEnumerable<PostListDto>> GetSubforumPostsAsync(long subforumId, string userId, PostsQueryDto postsQueryDto);

        Task<IEnumerable<PostSearchDto>> SearchPostsAsync(string searchTerm);

        Task<OperationResult<PostDetailsDto>> GetPostDetailsByIdForGuestUserAsync(long id);

        Task<OperationResult<PostDetailsDto>> GetPostDetailsByIdAsync(long id, string userId);

        Task<Post> CreatePostAsync(PostCreateDto postCreateDto,
            ApplicationUser user,
            Subforum subforum);

        Task<OperationResult> UpdatePostAsync(long postId, string userId, PostUpdateDto postUpdateDto);

        Task<OperationResult> DeletePostAsync(long postId, string userId);
    }
}
