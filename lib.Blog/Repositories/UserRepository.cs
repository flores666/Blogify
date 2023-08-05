using lib.Blog.DataAccess;
using lib.Blog.Interfaces;
using lib.Blog.Objects;

namespace lib.Blog.Repositories;

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