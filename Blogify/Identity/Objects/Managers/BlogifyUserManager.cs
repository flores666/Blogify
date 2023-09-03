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

    public async Task<bool> UpdateProfile(AppUser userData)
    {
        var user = await FindByIdAsync(userData.Id);
        if (user != null)
        {
            var result = await SetUserNameAsync(user, userData.UserName);
            if (result.Succeeded) 
            {
                user.FirstName = userData.FirstName;
                user.SecondName = userData.SecondName;
                user.Status = userData.Status;
                user.Description = userData.Description;
                
                result = await UpdateAsync(user);
                if (result.Succeeded) return true;
            }
        }

        return false;
    }

    public AppUser GetUserByName(string name)
    {
        return _db.Users.Where(u => u.UserName == name)
            .Include(u => u.Posts)
            .FirstOrDefault();
    }
}