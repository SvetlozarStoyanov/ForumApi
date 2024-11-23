using Database.Entities.Identity;
using Database.Entities.Subforums;

namespace Database.Entities.Relationships
{
    public class UserSubforum
    {
        public long Id { get; set; }
        public string UserId  { get; set; }
        public ApplicationUser User { get; set; }
        public long SubforumId { get; set; }
        public Subforum Subforum { get; set; }
    }
}
