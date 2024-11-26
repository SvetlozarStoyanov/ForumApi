namespace Models.DTOs.Posts.Input
{
    public class PostCreateDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public long SubforumId { get; set; }
    }
}
