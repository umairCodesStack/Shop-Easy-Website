using Ecommere_Website.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommere_Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult SignUp() 
        {
            return View("Auth");
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ViewBag.Signerror = "Email already Reistered";
                return View("Auth");
            }
            var user = new ApplicationUser
            {
                Name = model.Name,
                UserName=model.Email,
                Email = model.Email

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("HomePage", "Home");
            }
            string errors = string.Join("; ", result.Errors.Select(e => e.Description));
            ViewBag.Signerror = errors;
            return View("Auth");
        }
        public IActionResult LogIn() 
        {
            return View("Auth");
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(RegisterViewModel model)
        {

            
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ViewBag.Error= "Invalid Email or password.";
                return View("Auth");
            }
            
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("HomePage", "Home");
            }
            
            if (result.IsLockedOut)
            {
                ViewBag.Error= "Account is locked. Please try again later.";
            }
            else
            {
                ViewBag.Error= "Invalid login attempt.";
            }
            ViewBag.Error = "Invalid Credentials";
            return View("Auth");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LogIn","Account");
        }

    }
}
