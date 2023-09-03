using Blogify.Identity.Objects;
using Blogify.Models;
using lib.Blog.DataAccess;
using lib.Blog.Interfaces;
using lib.Blog.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogify.Controllers;

public class UserController : Controller
{
    private readonly IBlogRepository _blogRepository;
    private readonly BlogifySignInManager _signInManager;
    private readonly BlogifyUserManager _userManager;

    public UserController(IBlogRepository blogRepo, BlogifySignInManager signInManager,
        BlogifyUserManager userManager)
    {
        _blogRepository = blogRepo;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [Authorize]
    public IActionResult Index(string name)
    {
        var user = _userManager.GetUserByName(name);
        if (user == null) return NotFound();
        return View(user);
    }

    [HttpGet]
    public IActionResult CreatePost()
    {
        if (_signInManager.IsSignedIn(HttpContext.User)) return View();
        return RedirectToAction("Login", "Autorization");
    }

    [HttpPost]
    public IActionResult CreatePost(PostInputModel model)
    {
        if (ModelState.IsValid)
        {
            var post = model.CreatePost();
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            
            if (user != null)
            {
                post.AppUser = user;
                _blogRepository.SavePost(post);
                return RedirectToAction("Index", "User", new {name = user.UserName});
            }
        }

        return View();
    }

    [HttpGet]
    public IActionResult EditProfile()
    {
        var user = _userManager.GetUserAsync(HttpContext.User).Result;
        if (user == null) return NotFound();
        var model = new UserInputModel(user);
        if (model == null) return NotFound();
        return View("EditProfile", model);
    }

    [HttpPost]
    public IActionResult EditProfile(UserInputModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            
            var updatedUserData = new AppUser()
            {
                Id = user.Id,
                UserName = model.UserName,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                Status = model.Status,
                Description = model.Description
            };
            
            ViewBag.isSaved = _userManager.UpdateProfile(updatedUserData).Result;
        }

        return View();
    }
}