using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.CommentReplies;
using Contracts.Services.Entity.Comments;
using Contracts.Services.Managers;
using Database.Entities.Comments;
using Database.Entities.Posts;
using Microsoft.EntityFrameworkCore.Metadata;
using Models.Common;
using Models.Common.Enums;
using Models.DTOs.CommentReplies;
using Models.DTOs.Comments;
using Services.Entity.Comments;
using System.ComponentModel.Design;

namespace Services.Managers
{
    public class CommentReplyManager : ICommentReplyManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICommentReplyService commentReplyService;

        public CommentReplyManager(IUnitOfWork unitOfWork, ICommentReplyService commentReplyService)
        {
            this.unitOfWork = unitOfWork;
            this.commentReplyService = commentReplyService;
        }

        public async Task<OperationResult> CreateCommentReplyAsync(CommentReplyCreateDto commentReplyCreateDto, string userId)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var comment = await unitOfWork.CommentRepository.GetByIdAsync(commentReplyCreateDto.CommentId);

            if (comment is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"{nameof(Comment)} with id: {commentReplyCreateDto.CommentId} was not found!"));
                return operationResult;
            }

            await commentReplyService.CreateCommentReplyAsync(commentReplyCreateDto, user, comment);

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        public async Task<OperationResult> UpdateCommentReplyAsync(long commentReplyId, CommentReplyUpdateDto commentReplyUpdateDto, string userId)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }
            var updateCommentReplyResult = await commentReplyService.UpdateCommentReplyAsync(commentReplyId, commentReplyUpdateDto, userId);

            if (!updateCommentReplyResult.IsSuccessful)
            {
                operationResult.AppendErrors(updateCommentReplyResult);
                return operationResult;
            }

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        public async Task<OperationResult> DeleteCommentReplyAsync(long commentReplyId, string userId)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var deleteCommentResult = await commentReplyService.DeleteCommentReplyAsync(commentReplyId, userId);

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
