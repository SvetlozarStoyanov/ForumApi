using Database.Enums.Votes;
using Models.Common;
using Models.DTOs.CommentReplies.Input;

namespace Contracts.Services.Managers
{
    public interface ICommentReplyManager
    {
        Task<OperationResult<long>> CreateCommentReplyAsync(string userId,
            CommentReplyCreateDto commentReplyCreateDto);

        Task<OperationResult> UpdateCommentReplyAsync(long commentReplyId,
            string userId,
            CommentReplyUpdateDto commentReplyUpdateDto);

        Task<OperationResult> VoteOnCommentReplyAsync(long commentReplyId, string userId, CommentReplyVotes type);

        Task<OperationResult> DeleteCommentReplyAsync(long commentReplyId,
            string userId);
    }
}
