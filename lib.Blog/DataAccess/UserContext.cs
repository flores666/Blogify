using lib.Blog.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lib.Blog.DataAccess;

public class UserContext : IdentityDbContext<AppUser>
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var schema = "users";
        /*builder.Entity<IdentityRoleClaim<string>>().ToTable("role_claims", schema);
        builder.Entity<IdentityRole>().ToTable("roles", schema);
        builder.Entity<IdentityUserClaim<string>>().ToTable("user_claims", schema);
        builder.Entity<IdentityUserLogin<string>>().ToTable("user_logins", schema);
        builder.Entity<IdentityUserRole<string>>().ToTable("user_roles", schema);
        builder.Entity<IdentityUserToken<string>>().ToTable("user_tokens", schema);
        builder.Entity<AppUser>().ToTable("app_users", schema);*/
    }
}
