using Database.Entities.Comments;
using Database.Entities.Identity;
using Models.Common;
using Models.DTOs.CommentReplies;
using Models.DTOs.Comments;

namespace Contracts.Services.Entity.CommentReplies
{
    public interface ICommentReplyService
    {
        Task CreateCommentReplyAsync(CommentReplyCreateDto commentReplyCreateDto,
            ApplicationUser user,
            Comment comment);

        Task<OperationResult> UpdateCommentReplyAsync(long commentReplyId, CommentReplyUpdateDto commentReplyUpdateDto, string userId);

        Task<OperationResult> DeleteCommentReplyAsync(long commentReplyId, string userId);
    }
}
