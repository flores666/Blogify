using lib.Blog.Objects;

namespace Blogify.Identity.Objects;

public interface IUserManager
{
    public Task<bool> UpdateProfile(AppUser userData);
    public AppUser GetUserByName(string name);
}