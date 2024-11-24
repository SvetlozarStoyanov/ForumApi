using Database.Entities.Comments;
using Database.Entities.Identity;
using Models.Common;
using Models.DTOs.CommentReplies;

namespace Contracts.Services.Entity.CommentReplies
{
    public interface ICommentReplyService
    {
        Task CreateCommentReplyAsync(CommentReplyCreateDto commentReplyCreateDto,
            ApplicationUser user,
            Comment comment);

        Task<OperationResult> UpdateCommentReplyAsync(long commentReplyId, string userId, CommentReplyUpdateDto commentReplyUpdateDto);

        Task<OperationResult> DeleteCommentReplyAsync(long commentReplyId, string userId);
    }
}
