using Database.Entities.Comments;
using Database.Entities.Identity;
using Database.Entities.Posts;
using Database.Entities.Seeding;
using Database.Entities.Subforum;
using Database.Entities.Votes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class ForumDbContext : IdentityDbContext<ApplicationUser>
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }

        public DbSet<SeedEntity> SeedEntities { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentReply> CommentReplies { get; set; }
        public DbSet<PostVote> PostVotes { get; set; }
        public DbSet<CommentVote> CommentVotes { get; set; }
        public DbSet<Subforum> Subforums { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Comment>()
                .HasOne(x => x.Post)
                .WithMany(x => x.Comments)
                .OnDelete(DeleteBehavior.Restrict);            
            
            builder.Entity<Comment>()
                .HasMany(x => x.Replies)
                .WithOne(x => x.Comment)
                .OnDelete(DeleteBehavior.Restrict);
                        
            builder.Entity<Post>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Post)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Post>()
                .HasMany(x => x.Votes)
                .WithOne(x => x.Post)
                .OnDelete(DeleteBehavior.Restrict);            
            
            builder.Entity<Comment>()
                .HasMany(x => x.Votes)
                .WithOne(x => x.Comment)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Post>()
                .Property(x => x.Status)
                .HasConversion<string>();            
            
            builder.Entity<Comment>()
                .Property(x => x.Status)
                .HasConversion<string>();           
            
            builder.Entity<CommentReply>()
                .Property(x => x.Status)
                .HasConversion<string>();

            base.OnModelCreating(builder);
        }
    }
}
