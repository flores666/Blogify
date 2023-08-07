using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace lib.Blog.Objects;

[Table("app_users")]
public class AppUser : IdentityUser
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
    public ICollection<AppUser> Friends { get; set; } = new List<AppUser>();
    
    [DisplayName("Публикации")]
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}