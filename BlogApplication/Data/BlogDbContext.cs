using BlogApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Data;


public class BlogDbContext:DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base (options)
    {
    }
    
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Entry> Entries { get; set; }
}