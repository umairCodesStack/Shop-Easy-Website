using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UserAuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userStore;
    // IConfiguration is injected by ASP.NET Core's DI container
    public UserAuthController(IConfiguration configuration, IUserRepository userStore)
    {
        _configuration = configuration;
        _userStore = userStore;
    }
    [HttpPost("signup")]
    public IActionResult Signup([FromBody] AddUserDTO request)
    {
        // 1. Validate input
        if (request.UserName=="")
            return BadRequest(new { message = "Username is required" });

        if (string.IsNullOrWhiteSpace(request.Email))
            return BadRequest(new { message = "Email is required" });

        if (string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { message = "Password is required" });

        // 2. Validate email format
        if (!IsValidEmail(request.Email))
            return BadRequest(new { message = "Invalid email format" });

        // 3. Validate password strength
        if (!IsValidPassword(request.Password, out string passwordError))
            return BadRequest(new { message = passwordError });

        
        // 5. Check if email already exists
        if (_userStore.EmailExists(request.Email))
            return Conflict(new { message = "Email already exists" });

        // 6. Hash the password
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        AddUserDTO newUser = new AddUserDTO
        {
            UserName = request.UserName,
            Email = request.Email,
            Password = passwordHash,
            Role = request.Role
        };
        // 7. Create user
        var createdUser= _userStore.AddUser(newUser);
        Console.WriteLine("Created User ID: " + createdUser.Id);

        // 8. Generate JWT token for the new user
        var token = GenerateJwtToken(createdUser.Id.ToString(), createdUser.Name, createdUser.Email, createdUser.Role);

        return CreatedAtAction(nameof(Signup), new AuthResponse
        {
            AccessToken = token.tokenString,
            TokenType = "Bearer",
            ExpiresAt = token.expiresAt,
            Username = createdUser.Name,
            Email = createdUser.Email,
            Role = createdUser.Role
        });
    }

    // ===== LOGIN ENDPOINT =====
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // 1. Validate input
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { message = "Username and password are required" });

        // 2. Find user
        var user = _userStore.GetUserByEmail(request.Email);
        if (user == null)
            return Unauthorized(new { message = "Invalid username or password" });

        // 3. Verify password
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            return Unauthorized(new { message = "Invalid username or password" });

        // 4. Generate JWT token
        var token = GenerateJwtToken(user.Id.ToString(), user.Name, user.Email, user.Role);

        return Ok(new AuthResponse
        {
            AccessToken = token.tokenString,
            TokenType = "Bearer",
            ExpiresAt = token.expiresAt,
            Username = user.Name,
            Email = user.Email,
            Role = user.Role
        });
    }

    // ===== HELPER METHOD:  Generate JWT Token =====
    private (string tokenString, DateTime expiresAt) GenerateJwtToken(string userId, string username, string email, string role)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var secretKey = _configuration["Jwt:SecretKey"];
        var expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "60");

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid. NewGuid().ToString()),
            new Claim(ClaimTypes.Name, username),
            new Claim("userId", userId),
            new Claim(ClaimTypes.Role, role),
        };

        

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAt,
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return (tokenString, expiresAt);
    }

    // ===== HELPER METHOD:  Validate Email =====
    private bool IsValidEmail(string email)
    {
        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }

    // ===== HELPER METHOD: Validate Password =====
    private bool IsValidPassword(string password, out string errorMessage)
    {
        if (password.Length < 8)
        {
            errorMessage = "Password must be at least 8 characters long";
            return false;
        }

        if (!password.Any(char.IsUpper))
        {
            errorMessage = "Password must contain at least one uppercase letter";
            return false;
        }

        if (!password.Any(char.IsLower))
        {
            errorMessage = "Password must contain at least one lowercase letter";
            return false;
        }

        if (!password.Any(char.IsDigit))
        {
            errorMessage = "Password must contain at least one digit";
            return false;
        }

        errorMessage = string.Empty;
        return true;
    }
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}  

