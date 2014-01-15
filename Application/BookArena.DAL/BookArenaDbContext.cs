using System.Data.Entity;
using BookArena.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookArena.DAL
{
    public class BookArenaDbContext : IdentityDbContext<ApplicationUser>
    {
        public BookArenaDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Book> Book { get; set; }
    }
}