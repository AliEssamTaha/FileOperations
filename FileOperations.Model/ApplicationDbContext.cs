using Microsoft.EntityFrameworkCore;

namespace FileOperations.Model
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<File> Files{ get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<File>().Property(o => o.Content).IsRequired();
            modelBuilder.Entity<File>().Property(o => o.FileName).IsRequired();
            modelBuilder.Entity<File>().Property(o => o.FileExtension).IsRequired();
        }
    }
}
