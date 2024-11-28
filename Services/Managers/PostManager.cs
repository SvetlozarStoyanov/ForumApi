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

        public async Task<IEnumerable<PostListDto>> GetHomePagePostsForGuestUserAsync()
        {
            return await postService.GetHomePagePostsForGuestUserAsync();
        }

        public async Task<OperationResult<PostDetailsDto>> GetPostDetailsByIdAsync(long id)
        {
            return await postService.GetPostDetailsByIdAsync(id);
        }

        public async Task<OperationResult> CreatePostAsync(string userId, PostCreateDto postCreateDto)
        {
            var operationResult = new OperationResult();

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

            await postService.CreatePostAsync(postCreateDto, user, subforum);

            await unitOfWork.SaveChangesAsync();

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
