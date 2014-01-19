using System.Data.Entity;
using BookArena.Model;

namespace BookArena.DAL
{
    public class BookArenaDbContext : DbContext
    {
        public BookArenaDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Book> Book { get; set; }
    }
}