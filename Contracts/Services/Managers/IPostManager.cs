using Database.Enums.Votes;
using Models.Common;
using Models.DTOs.Posts.Input;
using Models.DTOs.Posts.Output;

namespace Contracts.Services.Managers
{
    public interface IPostManager
    {
        Task<OperationResult<IEnumerable<PostListDto>>> GetUserPostsAsync(string username, string? currentUserId, PostsQueryDto postsQueryDto);

        Task<OperationResult> VoteOnPostAsync(long postId, string userId, PostVotes type);

        Task<OperationResult<IEnumerable<PostListDto>>> GetHomePagePostsAsync(string? userId, PostsQueryDto postsQueryDto);

        Task<IEnumerable<PostSearchDto>> SearchPostsAsync(string searchTerm);

        Task<OperationResult<PostDetailsDto>> GetPostDetailsByIdAsync(long id, string? userId);

        Task<OperationResult<long>> CreatePostAsync(string userId, PostCreateDto postCreateDto);

        Task<OperationResult> UpdatePostAsync(long postId, string userId, PostUpdateDto postUpdateDto);

        Task<OperationResult> DeletePostAsync(long postId, string userId);

        Task<OperationResult<IEnumerable<PostListDto>>> GetSubforumPostsAsync(long subforumId, string userId, PostsQueryDto postsQueryDto);
    }
}
