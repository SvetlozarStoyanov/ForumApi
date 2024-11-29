using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Votes;
using Database.Entities.Comments;
using Database.Entities.Identity;
using Database.Entities.Votes;
using Database.Enums.Votes;

namespace Services.Entity.Votes
{
    public class CommentVoteService : ICommentVoteService
    {
        private readonly IUnitOfWork unitOfWork;

        public CommentVoteService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void VoteOnComment(CommentVotes type, Comment comment, ApplicationUser user)
        {
            var commentVoteBySameUser = comment.Votes.FirstOrDefault(x => x.UserId == user.Id);

            if (commentVoteBySameUser is not null)
            {
                unitOfWork.CommentVoteRepository.Delete(commentVoteBySameUser);
                if (commentVoteBySameUser.Type == type)
                {
                    return;
                }
            }

            var commentVote = new CommentVote()
            {
                Comment = comment,
                User = user,
                Type = type,
                CreatedOn = DateTime.UtcNow
            };

            comment.Votes.Add(commentVote);
        }
    }
}
