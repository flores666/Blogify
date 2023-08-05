using Blogify.Identity.Objects;
using lib.Blog;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogify.Identity.DataAccess;

public class UserContext : IdentityDbContext<User>
{
    public UserContext() { }
    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(Data.USERS_CONN_STRING).UseSnakeCaseNamingConvention(); 
    }
}