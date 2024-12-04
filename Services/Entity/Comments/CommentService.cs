using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Comments;
using Database.Entities.Comments;
using Database.Entities.Identity;
using Database.Entities.Posts;
using Database.Enums.Statuses;
using Database.Enums.Votes;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Common.Enums;
using Models.DTOs.CommentReplies.Output;
using Models.DTOs.Comments.Input;
using Models.DTOs.Comments.Output;
using Models.DTOs.UserPermittedActions.Output;
using Models.DTOs.Users.Output;
using Models.DTOs.Votes.Output;
using Models.Enums.Votes;

namespace Services.Entity.Comments
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CommentListDto>> GetPostCommentsForGuestUserAsync(long postId)
        {
            var postComments = await unitOfWork.CommentRepository
                .FindByConditionAsNoTracking(x => x.PostId == postId)
                .Select(x => new CommentListDto()
                {
                    Id = x.Id,
                    Text = x.Text,
                    User = new UserMinInfoDto()
                    {
                        Id = x.UserId,
                        Username = x.User.UserName!
                    },
                    VoteTally = x.Votes.Count(x => x.Type == CommentVotes.Up) - x.Votes.Count(x => x.Type == CommentVotes.Down),
                    Replies = x.Replies.Select(z => new CommentReplyListDto()
                    {
                        Id = z.Id,
                        Text = z.Text,
                        VoteTally = z.Votes.Count(x => x.Type == CommentReplyVotes.Up) - z.Votes.Count(x => x.Type == CommentReplyVotes.Down),
                        User = new UserMinInfoDto()
                        {
                            Id = z.UserId,
                            Username = z.User.UserName!
                        }
                    }),
                })
                .ToListAsync();

            return postComments;
        }

        public async Task<IEnumerable<CommentListDto>> GetPostCommentsAsync(long postId, string userId)
        {
            var postComments = await unitOfWork.CommentRepository
                .FindByConditionAsNoTracking(x => x.PostId == postId)
                .Select(x => new CommentListDto()
                {
                    Id = x.Id,
                    Text = x.Text,
                    User = new UserMinInfoDto()
                    {
                        Id = x.UserId,
                        Username = x.User.UserName!
                    },
                    VoteTally = x.Votes.Count(x => x.Type == CommentVotes.Up) - x.Votes.Count(x => x.Type == CommentVotes.Down),
                    Replies = x.Replies.Select(z => new CommentReplyListDto()
                    {
                        Id = z.Id,
                        Text = z.Text,
                        VoteTally = z.Votes.Count(x => x.Type == CommentReplyVotes.Up) - z.Votes.Count(x => x.Type == CommentReplyVotes.Down),
                        User = new UserMinInfoDto()
                        {
                            Id = z.UserId,
                            Username = z.User.UserName!
                        },
                        UserPermittedActions = new UserPermittedActionsDto()
                        {
                            CanEdit = z.UserId == userId,
                            CanDelete = z.UserId == userId
                        },
                        UserVote = new UserVoteDto()
                        {
                            VoteType = z.Votes.Any(z => z.Type == CommentReplyVotes.Up && z.UserId == userId) ? VoteTypes.Up :
                                z.Votes.Any(z => z.Type == CommentReplyVotes.Down && z.UserId == userId) ? VoteTypes.Down
                                : VoteTypes.None
                        }
                    }),
                    UserPermittedActions = new UserPermittedActionsDto()
                    {
                        CanEdit = x.UserId == userId,
                        CanDelete = x.UserId == userId
                    },
                    UserVote = new UserVoteDto()
                    {
                        VoteType = x.Votes.Any(x => x.Type == CommentVotes.Up && x.UserId == userId) ? VoteTypes.Up :
                                x.Votes.Any(x => x.Type == CommentVotes.Down && x.UserId == userId) ? VoteTypes.Down
                                : VoteTypes.None
                    }
                })
                .ToListAsync();

            return postComments;
        }

        public async Task<Comment> CreateCommentAsync(CommentCreateDto commentCreateDto, ApplicationUser user, Post post)
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

            return comment;
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
