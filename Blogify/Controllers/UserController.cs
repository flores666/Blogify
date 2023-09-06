using System.Security.Claims;
using Blogify.Identity.Objects;
using Blogify.Models;
using lib.Blog;
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
        var model = new UserViewModel(user.UserName, user.FirstName, user.SecondName, user.Status, user.Description);
        return View(model);
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
            var userName = _userManager.GetUserName(HttpContext.User);
            var user = _userManager.GetUserByName(userName);
            
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
    public async Task<IActionResult> EditProfile(UserInputModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            await _userManager.SetUserNameAsync(user, model.UserName);
            user.FirstName = model.FirstName;
            user.SecondName = model.SecondName;
            user.Status = model.Status;
            user.Description = model.Description;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) ViewBag.isSaved = true;
        }

        return View(model);
    }

    public async Task<IActionResult> GetUserDetailsJson()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        var userName = user.UserName;
        return Json(new {name = userName, link = Url.ActionLink("Index", "User", new {name = userName})});
    }

    [HttpPost]
    public IActionResult LoadPosts(int pageNumber = 0)
    {
        var userId = _userManager.GetUserId(HttpContext.User);
        if (string.IsNullOrEmpty(userId)) return new EmptyResult();
        var model = _blogRepository.GetUserLatestPosts(pageNumber, Data.pageSize, userId);
        return PartialView("_FeedPartial", model);
    }
}