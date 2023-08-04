using Microsoft.AspNetCore.Identity;

namespace Blogify.Identity.Objects;

public class Role : IdentityRole
{
    public Role() : base() { }
    public Role(string name) : base(name) { }
}