using Models.DTOs.Subforums.Output;
using Models.DTOs.UserPermittedActions.Output;
using Models.DTOs.Users.Output;
using Models.DTOs.Votes.Output;

namespace Models.DTOs.Posts.Output
{
    public class PostListDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int VoteTally { get; set; }
        public int CommentCount { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserMinInfoDto User { get; set; }
        public SubforumMinInfoDto Subforum { get; set; }
        public UserPermittedActionsDto UserPermittedActions { get; set; }
        public UserVoteDto? UserVote { get; set; }
    }
}
