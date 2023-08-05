using Blogify.Identity.DataAccess;
using Blogify.Identity.Interfaces;
using Blogify.Identity.Objects;

namespace Blogify.Identity.Repositories;

public class UserRepository : IUserRepository
{
    public User GetUser(string id)
    {
        using (var db = new UserContext())
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}