using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using lib.Blog.Objects;
using Microsoft.AspNetCore.Identity;

namespace Blogify.Identity.Objects;

public class User : IdentityUser
{
    [Column(TypeName = "text")]
    public string Description { get; set; }

    [DataType("varchar(25)")]
    public string FirstName { get; set; }
    
    [DataType("varchar(25)")]
    public string SecondName { get; set; }
    
    public ICollection<User> Friends { get; set; }
    public ICollection<Post> Posts { get; set; }
}