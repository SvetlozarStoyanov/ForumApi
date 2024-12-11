

namespace Models.DTOs.Subforums.Output
{
    public class SubforumDetailsDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool UserIsMember { get; set; }
        public long UserCount { get; set; }
    }
}
