using System.Diagnostics;
using Blogify.Models;
using lib.Blog.Interfaces;
using lib.Blog.Objects;
using Microsoft.AspNetCore.Mvc;

namespace Blogify.Controllers;

public class BlogController : Controller
{
    private readonly ILogger<BlogController> _logger;
    private readonly IBlogRepository _db;
    private int _pageSize = 10;

    public BlogController(ILogger<BlogController> logger, IBlogRepository repository)
    {
        _logger = logger;
        _db = repository;
    }

    public IActionResult Index(int pageNum = 0)
    {
        var model = new FeedViewModel(_db, 0, _pageSize);
        ViewData["Title"] = "Лента";
        return View(model);
    }

    public IActionResult Search(string search, int pageNum = 0)
    {
        var model = new FeedViewModel(_db, pageNum, _pageSize, search);
        ViewData["Title"] = $"Лента - найдено  {model.PostsNumber} записей";
        return View("Index", model);
    }

    [HttpPost]
    public JsonResult LoadMorePosts(int pageNumber)
    {
        var posts = _db.GetLatestPosts(pageNumber, _pageSize);
        return Json(posts);
    }
    
    [HttpPost]
    public PartialViewResult RenderAdditionalPost(PostViewModel model)
    {
        return PartialView("_PostPreview", model);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}