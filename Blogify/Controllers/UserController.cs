using Blogify.Identity.Objects;
using Blogify.Models;
using lib.Blog.DataAccess;
using lib.Blog.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogify.Controllers;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IBlogRepository _blogRepository;
    private readonly BlogifySignInManager _signInManager;
    private readonly BlogifyUserManager _userManager;

    public UserController(IUserRepository userRepo, IBlogRepository blogRepo, BlogifySignInManager signInManager,
        BlogifyUserManager userManager)
    {
        _userRepository = userRepo;
        _blogRepository = blogRepo;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [Authorize]
    public IActionResult Index(string name)
    {
        var user = _userRepository.GetUserByName(name);
        return View(user);
    }

    [HttpGet]
    public IActionResult CreatePost()
    {
        if (_signInManager.IsSignedIn(HttpContext.User)) return View();
        return RedirectToAction("Login", "Autorization");
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(PostInputModel model)
    {
        if (ModelState.IsValid)
        {
            var post = model.CreatePost();
            var userId = _userManager.GetUserId(HttpContext.User);

            var user = _userRepository.GetUserById(userId);
            
            if (user != null)
            {
                post.AppUser = user;
                _blogRepository.SavePost(post);
                return View("Index", user);
            }
        }

        return View();
    }

    [HttpPost]
    public IActionResult SetDescription(string value)
    {
        var name = _userManager.GetUserName(HttpContext.User);
        _userRepository.SetDescription(name, value);
        return RedirectToAction("Index", "User",new { name = name });
    }
}