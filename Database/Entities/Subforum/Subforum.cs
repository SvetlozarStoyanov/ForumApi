using Database.Entities.Identity;
using Database.Entities.Posts;

namespace Database.Entities.Subforum
{
    public class Subforum
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
