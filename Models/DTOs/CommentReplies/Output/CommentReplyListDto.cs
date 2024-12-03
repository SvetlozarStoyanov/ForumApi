using Models.DTOs.Users.Output;
using Models.DTOs.Votes.Output;

namespace Models.DTOs.CommentReplies.Output
{
    public class CommentReplyListDto
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public int VoteTally { get; set; }
        public UserMinInfoDto User { get; set; }
        public UserVoteDto? UserVote { get; set; }
    }
}
