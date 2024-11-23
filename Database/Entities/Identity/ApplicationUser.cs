using Database.Entities.Subforums;
using Microsoft.AspNetCore.Identity;

namespace Database.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Subforum> Subforums { get; set; }
        public ICollection<Subforum> AdministratedSubforums { get; set; }
    }
}
