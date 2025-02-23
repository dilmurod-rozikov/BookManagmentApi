using BookManagmentApi.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasQueryFilter(book => !book.IsDeleted);
        modelBuilder.Entity<Book>()
            .HasIndex(book => book.Title)
            .IsUnique();
    }
}
