namespace Models.DTOs.Comments
{
    public class CommentCreateDto
    {
        public long PostId { get; set; }
        public string Text { get; set; }
    }
}
