using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _24NEWSAPI.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}
