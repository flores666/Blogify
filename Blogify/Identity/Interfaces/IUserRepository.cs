using Blogify.Identity.Objects;

namespace Blogify.Identity.Interfaces;

public interface IUserRepository
{
    public User GetUser(string id);
}