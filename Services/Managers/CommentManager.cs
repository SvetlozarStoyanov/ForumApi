using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Comments;
using Contracts.Services.Entity.Votes;
using Contracts.Services.Managers;
using Database.Entities.Comments;
using Database.Entities.Posts;
using Database.Enums.Votes;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Common.Enums;
using Models.DTOs.Comments.Input;
using Models.DTOs.Comments.Output;

namespace Services.Managers
{
    public class CommentManager : ICommentManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICommentService commentService;
        private readonly ICommentVoteService commentVoteService;

        public CommentManager(IUnitOfWork unitOfWork,
            ICommentService commentService,
            ICommentVoteService commentVoteService)
        {
            this.unitOfWork = unitOfWork;
            this.commentService = commentService;
            this.commentVoteService = commentVoteService;
        }

        public async Task<OperationResult<IEnumerable<CommentListDto>>> GetPostCommentsAsync(long postId, string? userId)
        {
            var operationResult = new OperationResult<IEnumerable<CommentListDto>>();

            var post = await unitOfWork.PostRepository.GetByIdAsync(postId);

            if (post is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Post)} with id: {postId} was not found!"));
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

            var postComments = userId is not null ? await commentService.GetPostCommentsAsync(postId, userId) :
                await commentService.GetPostCommentsForGuestUserAsync(postId);

            operationResult.Data = postComments;

            return operationResult;
        }

        public async Task<OperationResult<long>> CreateCommentAsync(string userId, CommentCreateDto commentCreateDto)
        {
            var operationResult = new OperationResult<long>();

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

            var createdComment = await commentService.CreateCommentAsync(commentCreateDto, user, post);

            await unitOfWork.SaveChangesAsync();

            operationResult.Data = createdComment.Id;

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

        public async Task<OperationResult> VoteOnCommentAsync(long commentId, string userId, CommentVotes type)
        {
            var operationResult = new OperationResult();

            var comment = await unitOfWork.CommentRepository.FindByCondition(x => x.Id == commentId)
                .Include(x => x.Votes)
                .FirstOrDefaultAsync();

            if (comment is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Comment)} with id: {commentId} was not found!"));
                return operationResult;
            }

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            commentVoteService.VoteOnComment(type, comment, user);

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
