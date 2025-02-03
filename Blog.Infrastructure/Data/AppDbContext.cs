using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<BlogPostCategory> BlogPostCategories { get; set; }
    public DbSet<BlogPostTag> BlogPostTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // تنظیمات روابط Many-to-Many
        modelBuilder.Entity<BlogPostCategory>()
            .HasKey(bc => new { bc.BlogPostId, bc.CategoryId });
            
        modelBuilder.Entity<BlogPostTag>()
            .HasKey(bt => new { bt.BlogPostId, bt.TagId });
            
        // ایندکس برای Slug (برای بهبود عملکرد جستجو)
        modelBuilder.Entity<BlogPost>()
            .HasIndex(b => b.Slug)
            .IsUnique();
    }
}