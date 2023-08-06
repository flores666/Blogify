using lib.Blog.Objects;

namespace lib.Blog.Interfaces;

public interface IUserRepository
{
    public AppUser GetUser(string id);
    public void SavePost(Post post);
}