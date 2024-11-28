using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Votes;
using Database.Entities.Identity;
using Database.Entities.Posts;
using Database.Entities.Votes;
using Database.Enums.Votes;
using Models.Common;

namespace Services.Entity.Votes
{
    public class PostVoteService : IPostVoteService
    {
        private readonly IUnitOfWork unitOfWork;

        public PostVoteService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async void VoteOnPost(PostVotes type, Post post, ApplicationUser user)
        {
            var operationResult = new OperationResult();

            var postVoteBySameUser = post.Votes.FirstOrDefault(x => x.UserId == user.Id);

            if (postVoteBySameUser is not null)
            {
                unitOfWork.PostVoteRepository.Delete(postVoteBySameUser);
                if (postVoteBySameUser.Type == type)
                {
                    return;
                }
            }

            var postVote = new PostVote()
            {
                User = user,
                Post = post,
                Type = type
            };

            post.Votes.Add(postVote);  
        }
    }
}
