using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Comments;
using Contracts.Services.Managers;
using Database.Entities.Posts;
using Models.Common;
using Models.Common.Enums;
using Models.DTOs.Comments;
using System.Security.Cryptography.X509Certificates;

namespace Services.Managers
{
    public class CommentManager : ICommentManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICommentService commentService;

        public CommentManager(IUnitOfWork unitOfWork, ICommentService commentService)
        {
            this.unitOfWork = unitOfWork;
            this.commentService = commentService;
        }

        public async Task<OperationResult> CreateCommentAsync(string userId, CommentCreateDto commentCreateDto)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var post = await unitOfWork.PostRepository.GetByIdAsync(commentCreateDto.PostId);

            if (post is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Post)} with id: {commentCreateDto.PostId} was not found!"));
                return operationResult;
            }

            await commentService.CreateCommentAsync(commentCreateDto, user, post);

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        public async Task<OperationResult> UpdateCommentAsync(long commentId, string userId, CommentUpdateDto commentUpdateDto)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var updateCommentResult = await commentService.UpdateCommentAsync(commentId, userId, commentUpdateDto);

            if (!updateCommentResult.IsSuccessful)
            {
                operationResult.AppendErrors(updateCommentResult);
                return operationResult;
            }

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        public async Task<OperationResult> DeleteCommentAsync(long commentId, string userId)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var deleteCommentResult = await commentService.DeleteCommentAsync(commentId, userId);

            if (!deleteCommentResult.IsSuccessful)
            {
                operationResult.AppendErrors(deleteCommentResult);
                return operationResult;
            }

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }
    }
}
