using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using lib.Blog.Objects;
using lib.Extentions;

namespace Blogify.Models;

public class PostInputModel
{
    [Required(ErrorMessage = "This field is required")]
    [DisplayName("Заголовок")]
    public string Title { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [DisplayName("Содержание поста")]
    public string Text { get; set; }

    public List<string>? Tags { get; set; }

    public Post CreatePost()
    {
        return new Post()
        {
            Title = this.Title,
            Text = this.Text,
            UrlSlug = Title.GenerateSlug(),
            PostedOn = DateTime.Now.ToUniversalTime(),
            Published = true,
            Tags =  this.Tags.Select(n => new Tag()
            {
                Name = n,
                UrlSlug = n.GenerateSlug()
            }).ToList()
        };
    }
}