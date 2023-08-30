using lib.Blog.Objects;

namespace lib.Blog.Interfaces;

public interface IUserRepository
{
    public AppUser GetUserById(string userName);
    public AppUser GetUserByName(string name);
    public void SetDescription(string name, string value);
}