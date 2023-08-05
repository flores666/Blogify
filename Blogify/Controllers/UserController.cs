using Blogify.Identity.Interfaces;
using Blogify.Identity.Objects;
using lib.Blog.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogify.Controllers;

public class UserController : Controller
{
    private readonly IUserRepository _db;
    private readonly BlogifySignInManager _signInManager;

    public UserController(IUserRepository repo, BlogifySignInManager signInManager)
    {
        _db = repo;
        _signInManager = signInManager;
    }
    
    [Authorize]
    public IActionResult Index(string id)
    {
        var user = _db.GetUser(id);
        return View(user);
    }
    
    public IActionResult CreatePost()
    {
        if (_signInManager.IsSignedIn(HttpContext.User)) return View();
        return RedirectToAction("Login", "Autorization");
    }
}