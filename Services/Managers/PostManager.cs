using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Posts;
using Contracts.Services.Entity.Votes;
using Contracts.Services.Managers;
using Database.Entities.Posts;
using Database.Entities.Subforums;
using Database.Enums.Votes;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Common.Enums;
using Models.DTOs.Posts.Input;
using Models.DTOs.Posts.Output;

namespace Services.Managers
{
    public class PostManager : IPostManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPostService postService;
        private readonly IPostVoteService postVoteService;

        public PostManager(IUnitOfWork unitOfWork, IPostService postService, IPostVoteService postVoteService)
        {
            this.unitOfWork = unitOfWork;
            this.postService = postService;
            this.postVoteService = postVoteService;
        }

        public async Task<OperationResult<IEnumerable<PostListDto>>> GetUserPostsAsync(string username,
            string? currentUserId,
            PostsQueryDto postsQueryDto)
        {
            var operationResult = new OperationResult<IEnumerable<PostListDto>>();

            var user = await unitOfWork.UserRepository.FindByConditionAsNoTracking(x => x.UserName == username)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with username: {username} was not found!"));
                return operationResult;
            }

            if (currentUserId is not null)
            {
                var currentUser = await unitOfWork.UserRepository.GetByIdAsync(currentUserId);

                if (currentUser is null)
                {
                    operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {currentUserId} was not found!"));
                    return operationResult;
                }
            }

            var posts = currentUserId is not null ? await postService.GetUserPostsAsync(user.Id, currentUserId, postsQueryDto) 
                : await postService.GetUserPostsForGuestUserAsync(user.Id, postsQueryDto);

            operationResult.Data = posts;

            return operationResult;
        }

        public async Task<OperationResult<IEnumerable<PostListDto>>> GetHomePagePostsAsync(string? userId, PostsQueryDto postsQueryDto)
        {
            var operationResult = new OperationResult<IEnumerable<PostListDto>>();
            if (userId is not null)
            {
                var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

                if (user is null)
                {
                    operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                    return operationResult;
                }
            }

            operationResult.Data = userId is not null ? await postService.GetHomePagePostsAsync(userId, postsQueryDto)
                : await postService.GetHomePagePostsForGuestUserAsync(postsQueryDto);

            return operationResult;
        }

        public async Task<OperationResult<IEnumerable<PostListDto>>> GetSubforumPostsAsync(long subforumId, string userId, PostsQueryDto postsQueryDto)
        {
            var operationResult = new OperationResult<IEnumerable<PostListDto>>();
            var subforum = await unitOfWork.SubForumRepository.GetByIdAsync(subforumId);

            if (subforum is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Subforum)} with id: {subforumId} was not found!"));
                return operationResult;
            }

            if (userId is not null)
            {
                var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

                if (user is null)
                {
                    operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                    return operationResult;
                }
            }

            var fetchedPosts = userId is not null ? await postService.GetSubforumPostsAsync(subforumId, userId, postsQueryDto) :
                await postService.GetSubforumPostsForGuestUserAsync(subforumId, postsQueryDto);

            operationResult.Data = fetchedPosts;

            return operationResult;
        }

        public async Task<IEnumerable<PostSearchDto>> SearchPostsAsync(string searchTerm)
        {
            return await postService.SearchPostsAsync(searchTerm);
        }

        public async Task<OperationResult<PostDetailsDto>> GetPostDetailsByIdAsync(long id, string? userId)
        {
            var operationResult = new OperationResult<PostDetailsDto>();
            if (userId is not null)
            {
                var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

                if (user is null)
                {
                    operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                    return operationResult;
                }
            }

            var fetchPostDetailsResult = userId is not null ? await postService.GetPostDetailsByIdAsync(id, userId)
             : await postService.GetPostDetailsByIdForGuestUserAsync(id);

            if (!fetchPostDetailsResult.IsSuccessful)
            {
                operationResult.AppendErrors(fetchPostDetailsResult);
                return operationResult;
            }

            operationResult.Data = fetchPostDetailsResult.Data;

            return operationResult;
        }

        public async Task<OperationResult<long>> CreatePostAsync(string userId, PostCreateDto postCreateDto)
        {
            var operationResult = new OperationResult<long>();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var subforum = await unitOfWork.SubForumRepository.GetByIdAsync(postCreateDto.SubforumId);

            if (subforum is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Subforum)} with id: {postCreateDto.SubforumId} does note exist!"));
                return operationResult;
            }

            var createdPost = await postService.CreatePostAsync(postCreateDto, user, subforum);

            await unitOfWork.SaveChangesAsync();

            operationResult.Data = createdPost.Id;

            return operationResult;
        }

        public async Task<OperationResult> UpdatePostAsync(long postId,
            string userId,
            PostUpdateDto postUpdateDto)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var updatePostResult = await postService.UpdatePostAsync(postId, userId, postUpdateDto);

            if (!updatePostResult.IsSuccessful)
            {
                operationResult.AppendErrors(updatePostResult);
                return operationResult;
            }

            await unitOfWork.SaveChangesAsync();



            return operationResult;
        }

        public async Task<OperationResult> DeletePostAsync(long postId, string userId)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var editPostResult = await postService.DeletePostAsync(postId, userId);

            if (!editPostResult.IsSuccessful)
            {
                operationResult.AppendErrors(editPostResult);
                return operationResult;
            }

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        public async Task<OperationResult> VoteOnPostAsync(long postId, string userId, PostVotes type)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var post = await unitOfWork.PostRepository
                .FindByCondition(x => x.Id == postId)
                .Include(x => x.Votes)
                .FirstOrDefaultAsync();

            if (post is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Post)} with id: {postId} was not found!"));
                return operationResult;
            }

            postVoteService.VoteOnPost(type, post, user);

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }
    }
}
