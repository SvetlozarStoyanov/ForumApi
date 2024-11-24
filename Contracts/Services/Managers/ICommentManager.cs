using Models.Common;
using Models.DTOs.Comments;

namespace Contracts.Services.Managers
{
    public interface ICommentManager
    {
        Task<OperationResult> CreateCommentAsync(string userId,
            CommentCreateDto commentCreateDto);

        Task<OperationResult> UpdateCommentAsync(long commentId,
            string userId,
            CommentUpdateDto commentUpdateDto);

        Task<OperationResult> DeleteCommentAsync(long commentId, string userId);
    }
}
