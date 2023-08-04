using Blogify.Identity.Objects;
using Blogify.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogify.Controllers;

public class AutorizationController : Controller
{
    private readonly BlogifySignInManager _signInManager;
    private readonly BlogifyUserManager _userManager;
    private readonly ILogger<AutorizationController> _logger;
    private readonly IUserStore<User> _userStore;

    public AutorizationController(BlogifySignInManager signInManager, BlogifyUserManager userManager,
        ILogger<AutorizationController> logger, IUserStore<User> userStore)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _userStore = userStore;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, model.UserName, CancellationToken.None);
            user.TwoFactorEnabled = false;
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Blog");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
        }      
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout(string returnUrl)
    {
        await _signInManager.SignOutAsync();
        if (returnUrl != null)
        {
            return RedirectToPage(returnUrl);
        }
        else
        {
            return RedirectToAction("Index", "Blog");
        }
    }
    
    private User CreateUser()
    {
        try
        {
            return Activator.CreateInstance<User>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                                                $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively ");
        }
    }
}