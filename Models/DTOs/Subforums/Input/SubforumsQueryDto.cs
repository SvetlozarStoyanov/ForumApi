using Models.Enums.Subforums;

namespace Models.DTOs.Subforums.Input
{
    public class SubforumsQueryDto
    {
        public int Page { get; set; }
        public SubforumOrder Order { get; set; }
    }
}
