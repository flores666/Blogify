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

    public IActionResult Index()
    {
        var model = _db.GetLatestPosts(0, _pageSize);
        ViewData["Title"] = "Лента";
        return View(model);
    }

    public IActionResult Search(string search, int pageNum = 0)
    {
        var model = _db.GetLatestPostsForSearch(pageNum, _pageSize, search);
        ViewData["Title"] = $"Лента - запрос по поиску {search}";
        ViewBag.Query = search;
        return View("Index", model);
    }

    [HttpPost]
    public JsonResult GetPostsAsJSON(int pageNumber)
    {
        var posts = _db.GetLatestPosts(pageNumber, _pageSize);
        return Json(posts);
    }

    [HttpPost]
    public JsonResult GetPostsToSearch(int pageNumber, string query)
    {
        var posts = _db.GetLatestPostsForSearch(pageNumber, _pageSize, query);
        return Json(posts);
    }

    [HttpPost]
    public PartialViewResult DrawPosts(List<Post> posts)
    {
        return PartialView("_FeedPartial", posts);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}