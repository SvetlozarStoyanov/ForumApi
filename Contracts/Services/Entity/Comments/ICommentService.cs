using Database.Entities.Identity;
using Database.Entities.Posts;
using Models.Common;
using Models.DTOs.Comments;

namespace Contracts.Services.Entity.Comments
{
    public interface ICommentService
    {
        Task CreateCommentAsync(CommentCreateDto commentCreateDto,
            ApplicationUser user,
            Post post);

        Task<OperationResult> UpdateCommentAsync(long commentId, string userId, CommentUpdateDto commentUpdateDto);
       
        Task<OperationResult> DeleteCommentAsync(long commentId, string userId);
    }
}
