using lib.Blog.Objects;

namespace lib.Blog.Interfaces
{
    public interface IBlogRepository
    {
        public List<Post> GetLatestPosts(int pageNum, int pageSize);
        public int CountTotalPosts();
        public List<Post> GetLatestPostsForTag(int pageNum, int pageSize, string tagSlug);
        public List<Post> GetLatestPostsForSearch(int pageNum, int pageSize, string search);
        public Post ShowPost(int id);
        public void SavePost(Post post);
        public List<Post> GetUserLatestPosts(int pageNum, int pageSize, string userId);
    }
}
