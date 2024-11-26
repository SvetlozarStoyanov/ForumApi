namespace Models.DTOs.CommentReplies.Input
{
    public class CommentReplyCreateDto
    {
        public long CommentId { get; set; }
        public string Text { get; set; }
    }
}
