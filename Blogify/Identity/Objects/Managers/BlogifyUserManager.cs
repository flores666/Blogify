using System.Security.Claims;
using lib.Blog.DataAccess;
using lib.Blog.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Blogify.Identity.Objects;

public class BlogifyUserManager : UserManager<AppUser>, IUserManager
{
    private readonly IContext _db;
    
    public BlogifyUserManager(IUserStore<AppUser> store, 
        IOptions<IdentityOptions> optionsAccessor, 
        IPasswordHasher<AppUser> passwordHasher, 
        IEnumerable<IUserValidator<AppUser>> userValidators, 
        IEnumerable<IPasswordValidator<AppUser>> passwordValidators, 
        ILookupNormalizer keyNormalizer, 
        IdentityErrorDescriber errors, 
        IServiceProvider services, 
        ILogger<UserManager<AppUser>> logger,
        IContext context) : 
        base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
        _db = context;
    }

    public AppUser GetUserByName(string name)
    {
        return _db.Users.Where(u => u.UserName == name)
            .Include(u => u.Posts)
            .FirstOrDefault();
    }
}