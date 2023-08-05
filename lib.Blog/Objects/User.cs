using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace lib.Blog.Objects;

public class User : IdentityUser
{
    [Column(TypeName = "text")]
    [DisplayName("Описание")]
    public string? Description { get; set; }

    [DataType("varchar(25)")]
    [DisplayName("Имя")]
    public string? FirstName { get; set; }
    
    [DataType("varchar(25)")]
    [DisplayName("Фамилия")]
    public string? SecondName { get; set; }

    [DisplayName("Друзья")]
    public ICollection<User> Friends { get; set; } = new List<User>();
    
    [DisplayName("Публикации")]
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}