using Models.DTOs.CommentReplies.Output;
using Models.DTOs.Users.Output;

namespace Models.DTOs.Comments.Output
{
    public class CommentListDto
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public int VoteTally { get; set; }
        public UserMinInfoDto User { get; set; }
        public IEnumerable<CommentReplyListDto> Replies { get; set; }
    }
}
