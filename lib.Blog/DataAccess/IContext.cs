using lib.Blog.Objects;
using Microsoft.EntityFrameworkCore;

namespace lib.Blog.DataAccess;

public interface IContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<AppUser> Users { get; set; }
    public int SaveChanges();
}