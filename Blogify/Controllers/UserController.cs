using Microsoft.AspNetCore.Mvc;

namespace Blogify.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}