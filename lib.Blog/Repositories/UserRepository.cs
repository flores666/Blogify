using lib.Blog.DataAccess;
using lib.Blog.Interfaces;
using lib.Blog.Objects;
using Microsoft.EntityFrameworkCore;

namespace lib.Blog.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IContext _db;

    public UserRepository(IContext context)
    {
        _db = context;
    }

    public AppUser GetUser(string id)
    {
        return _db.Users.Where(u => u.Id == id)
            .Include(u => u.Posts)
            .FirstOrDefault();
    }
}