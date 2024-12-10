using Models.Enums.Posts;

namespace Models.DTOs.Posts.Output
{
    public class PostsQueryDto
    {
        public int Page { get; set; }
        public PostOrdering Order { get; set; }
    }
}
