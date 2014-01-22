using System.Data.Entity;
using BookArena.Model;
using BookArena.Model.EntityModel;

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
        public DbSet<Config> Config { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasRequired(t => t.Category)
                .WithMany(t => t.Books);
            base.OnModelCreating(modelBuilder);
        }
    }
}