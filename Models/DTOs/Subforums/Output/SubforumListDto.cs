namespace Models.DTOs.Subforums.Output
{
    public class SubforumListDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long MemberCount { get; set; }
        public bool UserIsMember { get; set; }
    }
}
