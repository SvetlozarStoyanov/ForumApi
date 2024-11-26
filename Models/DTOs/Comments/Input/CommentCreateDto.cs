namespace Models.DTOs.Comments.Input
{
    public class CommentCreateDto
    {
        public long PostId { get; set; }
        public string Text { get; set; }
    }
}
