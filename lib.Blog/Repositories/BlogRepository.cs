using lib.Blog.DataAccess;
using lib.Blog.Interfaces;
using lib.Blog.Objects;
using Microsoft.EntityFrameworkCore;

namespace lib.Blog.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private UserContext _db { get; set; } = new UserContext();

        /// <param name="pageNum">Starts from 0</param>
        public List<Post> GetLatestPosts(int pageNum, int pageSize)
        {
            return _db.Posts
                .Where(p => p.Published)
                .OrderByDescending(p => p.PostedOn)
                .Skip(pageNum * pageSize)
                .Take(pageSize)
                .Include(p => p.AppUser)
                .Include(p => p.Tags)
                .ToList();
        }

        public int CountTotalPosts()
        {
            return _db.Posts.Count(p => p.Published == true);
        }

        public List<Post> GetLatestPostsForTag(int pageNum, int pageSize, string tag)
        {
            return _db.Posts
                .Where(p => p.Published && p.Tags.Any(t => t.Name == tag))
                .OrderByDescending(p => p.PostedOn)
                .Skip(pageNum * pageSize)
                .Take(pageSize)
                .Include(p => p.AppUser)
                .Include(p => p.Tags)
                .ToList();
        }

        public List<Post> GetLatestPostsForSearch(int pageNum, int pageSize, string search)
        {
            return _db.Posts
                .Where(p => p.Published && (p.Tags.Any(t => t.Name == search) ||
                                            p.Title.Contains(search) ||
                                            p.Text.Contains(search)))
                .OrderByDescending(p => p.PostedOn)
                .Skip(pageNum * pageSize)
                .Take(pageSize)
                .Include(p => p.AppUser)
                .Include(p => p.Tags)
                .ToList();
        }

        public Post ShowPost(int id)
        {
            return _db.Posts.Where(p => p.Id == id).Include(p => p.Tags).FirstOrDefault();
        }

        public void SavePost(Post post)
        {
            _db.Posts.Add(post);
            _db.SaveChanges();
        }
    }

}
