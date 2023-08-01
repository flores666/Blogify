using lib.Blog.Interfaces;
using lib.Blog.Objects;

namespace Blogify.Models;

public class FeedViewModel
{
    public int PostsNumber { get; set; } = 0;
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public List<Post> Posts { get; set; }

    public FeedViewModel(IBlogRepository repo, int pageNum, int pageSize)
    {
        Posts = repo.GetLatestPosts(pageNum, pageSize);
        PostsNumber = Posts.Count;
        CurrentPage = pageNum;
    }

    public FeedViewModel(IBlogRepository repo, int pageNum, int pageSize, string search)
    {
        Posts = repo.GetLatestPostsForSearch(pageNum, pageSize, search);
        PostsNumber = Posts.Count;
        CurrentPage = pageNum;
    }
}