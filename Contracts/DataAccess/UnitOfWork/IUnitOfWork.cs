using Contracts.DataAccess.Repositories.Comments;
using Contracts.DataAccess.Repositories.Posts;
using Contracts.DataAccess.Repositories.Subforums;
using Contracts.DataAccess.Repositories.Users;
using Contracts.DataAccess.Repositories.Votes;

namespace Contracts.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        #region Repositories
        IUserRepository UserRepository { get; }
        IPostRepository PostRepository { get; }
        ICommentRepository CommentRepository { get; }
        ICommentReplyRepository CommentReplyRepository { get; }
        ISubForumRepository SubForumRepository { get; }
        IPostVoteRepository PostVoteRepository { get; }
        ICommentVoteRepository CommentVoteRepository { get; }
        ICommentReplyVoteRepository CommentReplyVoteRepository { get; }

        #endregion

        Task<int> SaveChangesAsync();
    }
}
