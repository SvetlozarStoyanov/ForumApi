using Models.DTOs.Users.Output;

namespace Models.DTOs.Posts.Output
{
    public class PostListDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int VoteTally { get; set; }
        public UserPostMinInfoDto User { get; set; }
    }
}
