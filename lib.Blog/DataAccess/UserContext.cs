using lib.Blog.Objects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lib.Blog.DataAccess;

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
        optionsBuilder.UseNpgsql(Data.CONN_STRING_MAIN).UseSnakeCaseNamingConvention(); 
    }
}