using lib.Blog.DataAccess;
using lib.Blog.Interfaces;
using lib.Blog.Objects;
using Microsoft.EntityFrameworkCore;

namespace lib.Blog.Repositories;

public class UserRepository : IUserRepository
{
    private UserContext _db = new UserContext();

    public AppUser GetUser(string id)
    {
        return _db.Users.Where(u => u.Id == id)
            .Include(u => u.Posts)
            .FirstOrDefault();
    }
    
    public void SavePost(Post post)
    {
        _db.Posts.Add(post); 
        _db.SaveChanges();
    }
}