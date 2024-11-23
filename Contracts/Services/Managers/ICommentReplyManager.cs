using Models.Common;
using Models.DTOs.CommentReplies;
using Models.DTOs.Comments;

namespace Contracts.Services.Managers
{
    public interface ICommentReplyManager
    {
        Task<OperationResult> CreateCommentReplyAsync(CommentReplyCreateDto commentReplyCreateDto,
            string userId);

        Task<OperationResult> UpdateCommentReplyAsync(long commentReplyId,
            CommentReplyUpdateDto commentReplyUpdateDto,
            string userId);

        Task<OperationResult> DeleteCommentReplyAsync(long commentReplyId,
            string userId);
    }
}
