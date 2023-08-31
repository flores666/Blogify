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
    private const int _pageSize = 10;

    public BlogController(ILogger<BlogController> logger, IBlogRepository repository)
    {
        _logger = logger;
        _db = repository;
    }
    
    public IActionResult Index(int pageNumber = 0)
    {
        var model = _db.GetLatestPosts(pageNumber, _pageSize);
        ViewData["Title"] = "Лента";
        ViewData["PageNum"] = pageNumber;
        return Request.IsAjaxRequest() ? PartialView("_FeedPartial", model) : View(model);
    }

    public IActionResult Search(string query, string? tag = null, int pageNumber = 0)
    {
        var model = new List<Post>();
        if (!string.IsNullOrEmpty(query))
        {
            model = _db.GetLatestPostsForSearch(pageNumber, _pageSize, query);
            ViewData["Title"] = $"Лента - запрос по поиску {query}";
            ViewBag.Query = query;
        }
        
        if (!string.IsNullOrEmpty(tag))
        {
            model = _db.GetLatestPostsForTag(pageNumber, _pageSize, tag);
            ViewData["Title"] = $"Лента - запрос по тегу {tag}";
            ViewBag.Tag = tag;
        }
        
        ViewData["PageNum"] = pageNumber;
        return Request.IsAjaxRequest() ? PartialView("_FeedPartial", model) : View("Index", model);
    }

    public PartialViewResult Post(int id)
    {
        var post = _db.ShowPost(id);
        return PartialView("_PostPartial", post);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}