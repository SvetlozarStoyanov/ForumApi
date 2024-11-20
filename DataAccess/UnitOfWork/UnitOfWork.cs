using Contracts.DataAccess.Repositories.Comments;
using Contracts.DataAccess.Repositories.Posts;
using Contracts.DataAccess.Repositories.Subforums;
using Contracts.DataAccess.Repositories.Users;
using Contracts.DataAccess.Repositories.Votes;
using Contracts.DataAccess.UnitOfWork;
using DataAccess.Repositories.Comments;
using DataAccess.Repositories.Posts;
using DataAccess.Repositories.Subforums;
using DataAccess.Repositories.Users;
using DataAccess.Repositories.Votes;
using Database;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ForumDbContext context;

        #region Repository fields
        private IUserRepository userRepository;
        private IPostRepository postRepository;
        private ICommentRepository commentRepository;
        private ICommentReplyRepository commentReplyRepository;
        private ISubForumRepository subForumRepository;
        private ICommentVoteRepository commentVoteRepository;
        private IPostVoteRepository postVoteRepository;
        #endregion

        public UnitOfWork(ForumDbContext context)
        {
            this.context = context;
        }


        #region Repositories
        public IUserRepository UserRepository => userRepository ??= new UserRepository(context);
        public IPostRepository PostRepository => postRepository ??= new PostRepository(context);

        public ICommentRepository CommentRepository => commentRepository ??= new CommentRepository(context);

        public ICommentReplyRepository CommentReplyRepository => commentReplyRepository ??= new CommentReplyRepository(context);

        public ISubForumRepository SubForumRepository => subForumRepository ??= new SubForumRepository(context);

        public IPostVoteRepository PostVoteRepository => postVoteRepository ??= new PostVoteRepository(context);

        public ICommentVoteRepository CommentVoteRepository => commentVoteRepository ??= new CommentVoteRepository(context);

        #endregion
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
