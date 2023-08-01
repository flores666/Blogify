using lib.Blog.Objects;

namespace Blogify.Models;

public class PostViewModel
{
    public int Index { get; set; }
    public Post Post { get; set; }

    public PostViewModel(Post post, int index)
    {
        Post = post;
        Index = index;
    }

    public PostViewModel() { }
}