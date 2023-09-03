using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using lib.Blog.Objects;

namespace Blogify.Models;

public class UserInputModel
{
    [DisplayName("Никнейм")]
    [MaxLength(50)]
    public string UserName { get; set; }

    [DisplayName("Имя")]
    [MaxLength(25)]
    public string? FirstName { get; set; } 
    
    [DisplayName("Фамилия")]
    [MaxLength(25)]
    public string? SecondName { get; set; }
    
    [DisplayName("Статус")]
    [MaxLength(40)]
    public string? Status { get; set; }
    
    [DisplayName("Описание страницы")]
    public string? Description { get; set; }

    public UserInputModel() { }
    
    public UserInputModel(AppUser user)
    {
        UserName = user.UserName;
        FirstName = user.FirstName?.ToString();
        SecondName = user.SecondName?.ToString();
        Status = user.Status?.ToString();
        Description = user.Description?.ToString();
    }
}