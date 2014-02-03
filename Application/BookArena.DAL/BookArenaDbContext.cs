using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BookArena.Model.EntityModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookArena.DAL
{
    public class BookArenaDbContext : IdentityDbContext<ApplicationUser>
    {
        public BookArenaDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookMetaData> BookMetaData { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Category>()
                .HasMany(t => t.Books);
            base.OnModelCreating(modelBuilder);
        }
    }
}