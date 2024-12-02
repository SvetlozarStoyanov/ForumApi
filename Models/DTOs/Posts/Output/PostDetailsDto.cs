using Models.DTOs.Comments.Output;
using Models.DTOs.Subforums.Output;
using Models.DTOs.Users.Output;

namespace Models.DTOs.Posts.Output
{
    public class PostDetailsDto
    {
        public PostListDto Post { get; set; }
        public UserMinInfoDto User { get; set; }
        public SubforumMinInfoDto Subforum { get; set; }
        public IEnumerable<CommentListDto> Comments { get; set; }
    }
}
