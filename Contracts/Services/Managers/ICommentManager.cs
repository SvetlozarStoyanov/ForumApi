using Database.Enums.Votes;
using Models.Common;
using Models.DTOs.Comments.Input;

namespace Contracts.Services.Managers
{
    public interface ICommentManager
    {
        Task<OperationResult> CreateCommentAsync(string userId,
            CommentCreateDto commentCreateDto);

        Task<OperationResult> UpdateCommentAsync(long commentId,
            string userId,
            CommentUpdateDto commentUpdateDto);

        Task<OperationResult> VoteOnCommentAsync(long commentId, string userId, CommentVotes type);

        Task<OperationResult> DeleteCommentAsync(long commentId, string userId);
    }
}
