using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure;
using Domain.DTOs;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterViewModel model)
    {
        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
        {
            return BadRequest(new SignupResponseDTO{ message = "Email already Registered", status=false });
        }

        var user = new ApplicationUser
        {
            Name = model.Name,
            UserName = model.Email,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await  _signInManager.SignInAsync(user, isPersistent: false);
            return Ok(new SignupResponseDTO{ message = "User registered successfully", status=true});
        }

        string errors = string.Join("; ", result.Errors.Select(e => e.Description));
        return BadRequest(new { error = errors });
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return Unauthorized(new SignupResponseDTO{ message = "Invalid Email or password.", status = false });
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return Ok(new SignupResponseDTO{ message = "Login successful", status=true ,userId=user.Id});
        }

        if (result.IsLockedOut)
        {
            return Unauthorized(new { error = "Account is locked. Please try again later." });
        }

        return Unauthorized(new { error = "Invalid Credentials" });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { message = "Logged out successfully" });
    }
}
