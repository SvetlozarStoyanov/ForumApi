using Models.Common;
using Models.DTOs.Comments;

namespace Contracts.Services.Managers
{
    public interface ICommentManager
    {
        Task<OperationResult> CreateCommentAsync(CommentCreateDto commentCreateDto, string userId);

        Task<OperationResult> UpdateCommentAsync(long commentId, CommentUpdateDto commentUpdateDto, string userId);

        Task<OperationResult> DeleteCommentAsync(long commentId, string userId);
    }
}
