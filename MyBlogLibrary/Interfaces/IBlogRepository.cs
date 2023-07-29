using MyBlogLibrary.Objects;

namespace MyBlogLibrary.Interfaces
{
    public interface IBlogRepository
    {
        public IList<Post> ShowLatestPosts(int pageNum, int pageSize);
        public int NumberTotalPosts();

        public IList<Post> ShowLatestPostsForTag(int pageNum, int pageSize, string tagSlug);
        public int NumberTotalPostsForTag(string tagSlug);

        public IList<Post> ShowLatestPostsForSearch(int pageNum, int pageSize, string search);
        public int NumberTotalPostsForSearch(string search);

        public Post ShowSinglePost(int id);
    }
}
