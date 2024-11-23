using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Posts;
using Contracts.Services.Managers;
using Database.Entities.Subforums;
using Models.Common;
using Models.Common.Enums;
using Models.DTOs.Posts;

namespace Services.Managers
{
    public class PostManager : IPostManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPostService postService;

        public PostManager(IUnitOfWork unitOfWork, IPostService postService)
        {
            this.unitOfWork = unitOfWork;
            this.postService = postService;
        }

        public async Task<OperationResult> CreatePostAsync(PostCreateDto postCreateDto, string userId)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var subforum = await unitOfWork.SubForumRepository.GetByIdAsync(postCreateDto.SubforumId);

            if (subforum is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"{nameof(Subforum)} with id: {postCreateDto.SubforumId} does note exist!"));
                return operationResult;
            }

            await postService.CreatePostAsync(postCreateDto, user, subforum);

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        public async Task<OperationResult> UpdatePostAsync(long postId,
            PostUpdateDto postUpdateDto,
            string userId)
        {
            var operationResult = new OperationResult();

            var author = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (author is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var updatePostResult = await postService.UpdatePostAsync(postId, postUpdateDto, userId);

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
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
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
    }
}
