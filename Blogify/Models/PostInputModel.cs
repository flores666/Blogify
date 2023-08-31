using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using lib.Blog.Objects;
using lib.Extensions;

namespace Blogify.Models;

public class PostInputModel
{
    [DisplayName("Заголовок")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [DisplayName("Содержание поста")]
    public string Text { get; set; }
    
    [DisplayName("Теги")]
    [TagValidation(20,  @"[^\p{L}\p{N}]", ErrorMessage = "Invalid tag name")]
    public List<string>? Tags { get; set; } = new List<string>();

    public Post CreatePost()
    {
        return new Post()
        {
            Title = this.Title,
            Text = this.Text,
            UrlSlug = Title.GenerateSlug(),
            PostedOn = DateTime.Now.ToUniversalTime(),
            Published = true,
            Tags = this.Tags.Select(n => new Tag()
            {
                Name = n,
                UrlSlug = n.GenerateSlug()
            }).Where(t => t.Name != null).ToList()
        };
    }
}