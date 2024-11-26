using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Comments;
using Database.Entities.Comments;
using Database.Entities.Identity;
using Database.Entities.Posts;
using Database.Enums.Statuses;
using Models.Common;
using Models.Common.Enums;
using Models.DTOs.Comments.Input;

namespace Services.Entity.Comments
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateCommentAsync(CommentCreateDto commentCreateDto, ApplicationUser user, Post post)
        {
            var operationResult = new OperationResult();

            var currDate = DateTime.UtcNow;

            var comment = new Comment()
            {
                Text = commentCreateDto.Text,
                CreatedOn = currDate,
                UpdatedOn = currDate,
                Post = post,
                User = user,
            };

            await unitOfWork.CommentRepository.AddAsync(comment);
        }

        public async Task<OperationResult> UpdateCommentAsync(long commentId, string userId, CommentUpdateDto commentUpdateDto)
        {
            var operationResult = new OperationResult();

            var comment = await unitOfWork.CommentRepository.GetByIdAsync(commentId);

            if (comment is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Comment)} with id: {commentId} was not found!"));
                return operationResult;
            }

            if (comment.UserId != userId)
            {
                operationResult.AddError(new Error(ErrorTypes.BadRequest, $"{nameof(Comment)} with id: {commentId} does not belong to User with id: {userId}"));
                return operationResult;
            }

            comment.Text = commentUpdateDto.Text;
            comment.UpdatedOn = DateTime.UtcNow;

            return operationResult;
        }
        
        public async Task<OperationResult> DeleteCommentAsync(long commentId, string userId)
        {
            var operationResult = new OperationResult();

            var comment = await unitOfWork.CommentRepository.GetByIdAsync(commentId);

            if (comment is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Comment)} with id: {commentId} was not found!"));
                return operationResult;
            }

            if (comment.UserId != userId)
            {
                operationResult.AddError(new Error(ErrorTypes.BadRequest, $"{nameof(Comment)} with id: {commentId} does not belong to User with id: {userId}"));
                return operationResult;
            }

            comment.Status = EntityStatus.Archived;
            comment.UpdatedOn = DateTime.UtcNow;

            return operationResult;
        }
    }
}
