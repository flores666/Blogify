using lib.Blog.Objects;

namespace Blogify.Identity.Objects;

public interface IUserManager
{
    public AppUser GetUserByName(string name);
}