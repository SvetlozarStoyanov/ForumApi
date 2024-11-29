using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.CommentReplies;
using Contracts.Services.Entity.Votes;
using Contracts.Services.Managers;
using Database.Entities.Comments;
using Database.Enums.Votes;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Common.Enums;
using Models.DTOs.CommentReplies.Input;

namespace Services.Managers
{
    public class CommentReplyManager : ICommentReplyManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICommentReplyService commentReplyService;
        private readonly ICommentReplyVoteService commentReplyVoteService;

        public CommentReplyManager(IUnitOfWork unitOfWork,
            ICommentReplyService commentReplyService,
            ICommentReplyVoteService commentReplyVoteService)
        {
            this.unitOfWork = unitOfWork;
            this.commentReplyService = commentReplyService;
            this.commentReplyVoteService = commentReplyVoteService;
        }

        public async Task<OperationResult> CreateCommentReplyAsync(string userId,
            CommentReplyCreateDto commentReplyCreateDto)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var comment = await unitOfWork.CommentRepository.GetByIdAsync(commentReplyCreateDto.CommentId);

            if (comment is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Comment)} with id: {commentReplyCreateDto.CommentId} was not found!"));
                return operationResult;
            }

            await commentReplyService.CreateCommentReplyAsync(commentReplyCreateDto, user, comment);

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        public async Task<OperationResult> UpdateCommentReplyAsync(long commentReplyId, string userId, CommentReplyUpdateDto commentReplyUpdateDto)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var updateCommentReplyResult = await commentReplyService.UpdateCommentReplyAsync(commentReplyId, userId, commentReplyUpdateDto);

            if (!updateCommentReplyResult.IsSuccessful)
            {
                operationResult.AppendErrors(updateCommentReplyResult);
                return operationResult;
            }

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        public async Task<OperationResult> VoteOnCommentReplyAsync(long commentReplyId, string userId, CommentReplyVotes type)
        {
            var operationResult = new OperationResult();
            
            var commentReply = await unitOfWork.CommentReplyRepository.FindByCondition(x => x.Id == commentReplyId)
                .Include(x => x.Votes)
                .FirstOrDefaultAsync();

            if (commentReply is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(CommentReply)} with id: {commentReplyId} was not found!"));
                return operationResult;
            }

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            commentReplyVoteService.VoteOnCommentReply(type, commentReply, user);

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        public async Task<OperationResult> DeleteCommentReplyAsync(long commentReplyId, string userId)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
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
