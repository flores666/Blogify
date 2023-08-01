using lib.Blog.Objects;

namespace Blogify.Models;

public class PostViewModel
{
    public bool IsEven { get; set; }
    public Post Post { get; set; }

    public PostViewModel(Post post, bool isEven)
    {
        Post = post;
        IsEven = isEven;
    }

    public PostViewModel() { }
}