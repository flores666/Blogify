using lib.Blog.Objects;

namespace lib.Blog.Interfaces;

public interface IUserRepository
{
    public User GetUser(string id);
}