using Database.Enums.Votes;
using Models.Common;
using Models.DTOs.Posts.Input;
using Models.DTOs.Posts.Output;

namespace Contracts.Services.Managers
{
    public interface IPostManager
    {
        Task<OperationResult> VoteOnPostAsync(long postId, string userId, PostVotes type);

        Task<IEnumerable<PostListDto>> GetHomePagePostsForGuestUserAsync();

        Task<OperationResult<PostDetailsDto>> GetPostDetailsByIdAsync(long id);
        
        Task<OperationResult> CreatePostAsync(string userId, PostCreateDto postCreateDto);

        Task<OperationResult> UpdatePostAsync(long postId, string userId, PostUpdateDto postUpdateDto);

        Task<OperationResult> DeletePostAsync(long postId, string userId);

    }
}
