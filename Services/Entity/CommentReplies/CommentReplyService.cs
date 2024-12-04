using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.CommentReplies;
using Database.Entities.Comments;
using Database.Entities.Identity;
using Database.Enums.Statuses;
using Models.Common;
using Models.Common.Enums;
using Models.DTOs.CommentReplies.Input;

namespace Services.Entity.CommentReplies
{
    public class CommentReplyService : ICommentReplyService
    {
        private readonly IUnitOfWork unitOfWork;

        public CommentReplyService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<CommentReply> CreateCommentReplyAsync(CommentReplyCreateDto commentReplyCreateDto, ApplicationUser user, Comment comment)
        {
            var currDate = DateTime.UtcNow;

            var commentReply = new CommentReply()
            {
                Text = commentReplyCreateDto.Text,
                UpdatedOn = currDate,
                CreatedOn = currDate,
                Comment = comment,
                User = user,
            };

            await unitOfWork.CommentReplyRepository.AddAsync(commentReply);

            return commentReply;
        }

        public async Task<OperationResult> UpdateCommentReplyAsync(long commentReplyId, string userId, CommentReplyUpdateDto commentReplyUpdateDto)
        {
            var operationResult = new OperationResult();

            var comment = await unitOfWork.CommentReplyRepository.GetByIdAsync(commentReplyId);

            if (comment is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(CommentReply)} with id: {commentReplyId} was not found!"));
                return operationResult;
            }

            if (comment.UserId != userId)
            {
                operationResult.AddError(new Error(ErrorTypes.BadRequest, $"{nameof(CommentReply)} with id: {commentReplyId} does not belong to User with id: {userId}"));
                return operationResult;
            }

            comment.Text = commentReplyUpdateDto.Text;
            comment.UpdatedOn = DateTime.UtcNow;

            return operationResult;
        }

        public async Task<OperationResult> DeleteCommentReplyAsync(long commentReplyId, string userId)
        {
            var operationResult = new OperationResult();

            var commentReply = await unitOfWork.CommentReplyRepository.GetByIdAsync(commentReplyId);

            if (commentReply is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(CommentReply)} with id: {commentReplyId} was not found!"));
                return operationResult;
            }

            if (commentReply.UserId != userId)
            {
                operationResult.AddError(new Error(ErrorTypes.BadRequest, $"{nameof(CommentReply)} with id: {commentReplyId} does not belong to User with id: {userId}"));
                return operationResult;
            }

            commentReply.Status = EntityStatus.Archived;
            commentReply.UpdatedOn = DateTime.UtcNow;

            return operationResult;
        }
    }
}
