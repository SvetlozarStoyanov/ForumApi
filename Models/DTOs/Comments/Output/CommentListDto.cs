using Models.DTOs.CommentReplies.Output;
using Models.DTOs.UserPermittedActions.Output;
using Models.DTOs.Users.Output;
using Models.DTOs.Votes.Output;

namespace Models.DTOs.Comments.Output
{
    public class CommentListDto
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public int VoteTally { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserMinInfoDto User { get; set; }
        public UserVoteDto? UserVote { get; set; }
        public UserPermittedActionsDto UserPermittedActions { get; set; }
        public IEnumerable<CommentReplyListDto> Replies { get; set; }
    }
}
