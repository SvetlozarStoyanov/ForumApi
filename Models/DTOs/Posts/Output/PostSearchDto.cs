using Models.DTOs.Subforums.Output;

namespace Models.DTOs.Posts.Output
{
    public class PostSearchDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public SubforumMinInfoDto Subforum { get; set; }
    }
}
