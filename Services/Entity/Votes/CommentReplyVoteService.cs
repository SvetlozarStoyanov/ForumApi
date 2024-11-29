using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Votes;
using Database.Entities.Comments;
using Database.Entities.Identity;
using Database.Entities.Votes;
using Database.Enums.Votes;

namespace Services.Entity.Votes
{
    public class CommentReplyVoteService : ICommentReplyVoteService
    {
        private readonly IUnitOfWork unitOfWork;

        public CommentReplyVoteService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void VoteOnCommentReply(CommentReplyVotes type, CommentReply commentReply, ApplicationUser user)
        {
            var voteOnSameCommentReply = commentReply.Votes.FirstOrDefault(x => x.UserId == user.Id);

            if (voteOnSameCommentReply is not null)
            {
                unitOfWork.CommentReplyVoteRepository.Delete(voteOnSameCommentReply);
                if (voteOnSameCommentReply.Type == type)
                {
                    return;
                }
            }

            var commentReplyVote = new CommentReplyVote()
            {
                CreatedOn = DateTime.UtcNow,
                User = user,
                Type = type
            };

            commentReply.Votes.Add(commentReplyVote);
        }
    }
}
