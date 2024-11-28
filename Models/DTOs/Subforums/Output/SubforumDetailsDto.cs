using Models.DTOs.Posts.Output;

namespace Models.DTOs.Subforums.Output
{
    public class SubforumDetailsDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PostListDto> Posts { get; set; }
    }
}
