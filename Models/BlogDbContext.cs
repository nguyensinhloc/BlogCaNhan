using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlogCaNhan.Models;

namespace BlogCaNhan.Models
{
    public class BlogDbContext : IdentityDbContext<AppUser>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<BlogCaNhan.Models.Login> Login { get; set; }
    }
}
