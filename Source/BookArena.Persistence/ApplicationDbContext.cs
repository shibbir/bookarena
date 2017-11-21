using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.Configuration;
using BookArena.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookArena.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(WebConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString)
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

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}