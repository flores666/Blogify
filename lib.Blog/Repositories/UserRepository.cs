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

    public AppUser GetUserById(string id)
    {
        return _db.Users.Where(u => u.Id == id)
            .Include(u => u.Posts)
            .FirstOrDefault();
    }
    
    public AppUser GetUserByName(string name)
    {
        return _db.Users.Where(u => u.UserName == name)
            .Include(u => u.Posts)
            .FirstOrDefault();
    }

    public void SetDescription(string name, string value)
    {
        var user = _db.Users.FirstOrDefault(u => u.UserName == name);
        user.Description = value;
        _db.Users.Update(user);
        _db.SaveChanges();
    }
}