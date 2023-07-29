using Microsoft.EntityFrameworkCore;
using MyBlog;
using MyBlogLibrary.DataAccess;
using MyBlogLibrary.Interfaces;
using MyBlogLibrary.Objects;

namespace MyBlogLibrary.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        public IList<Post> ShowLatestPosts(int pageNum, int pageSize)
        {
            using (var db = new BlogContext())
            {
                return db.Posts
                    .Where(p => p.Published)
                    .OrderByDescending(p => p.PostedOn)
                    .Skip(pageNum * pageSize)
                    .Take(pageSize)
                    .Include(p => p.Tags)
                    .ToList();
            }
        }

        public int NumberTotalPosts()
        {
            using (var db = new BlogContext())
            {
                return db.Posts
                    .Where(p => p.Published)
                    .Count();
            }
        }

        public IList<Post> ShowLatestPostsForTag(int pageNum, int pageSize, string tagSlug)
        {
            using (var db = new BlogContext())
            {
                return db.Posts
                    .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Contains(tagSlug)))
                    .OrderByDescending(p => p.PostedOn)
                    .Skip(pageNum * pageSize)
                    .Take(pageSize)
                    .Include(p => p.Tags)
                    .ToList();
            }
        }

        public int NumberTotalPostsForTag(string tagSlug)
        {
            using (var db = new BlogContext())
            {
                return db.Posts
                    .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Contains(tagSlug)))
                    .Count();
            }
        }

        public IList<Post> ShowLatestPostsForSearch(int pageNum, int pageSize, string search)
        {
            var slug = search.GenerateSlug();
            using (var db = new BlogContext())
            {
                return db.Posts
                    .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Contains(slug)) ||
                                                p.Title.Contains(search) ||
                                                p.Text.Contains(search))
                    .OrderByDescending(p => p.PostedOn)
                    .Skip(pageNum * pageSize)
                    .Take(pageSize)
                    .Include(p => p.Tags)
                    .ToList();
            }
        }

        public int NumberTotalPostsForSearch(string search)
        {
            var slug = search.GenerateSlug();
            using (var db = new BlogContext())
            {
                return db.Posts
                    .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Contains(slug)) ||
                                                p.Title.Contains(search) ||
                                                p.Text.Contains(search))
                    .Count();
            }
        }

        public Post ShowSinglePost(int id)
        {
            using (var db = new BlogContext())
            {
                return db.Posts
                    .Where(p => p.Id == id).FirstOrDefault();
            }
        }
    }
}
