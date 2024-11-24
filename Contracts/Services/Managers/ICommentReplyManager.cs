using Models.Common;
using Models.DTOs.CommentReplies;

namespace Contracts.Services.Managers
{
    public interface ICommentReplyManager
    {
        Task<OperationResult> CreateCommentReplyAsync(string userId,
            CommentReplyCreateDto commentReplyCreateDto);

        Task<OperationResult> UpdateCommentReplyAsync(long commentReplyId,
            string userId,
            CommentReplyUpdateDto commentReplyUpdateDto);

        Task<OperationResult> DeleteCommentReplyAsync(long commentReplyId,
            string userId);
    }
}
