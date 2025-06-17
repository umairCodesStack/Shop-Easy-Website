using System.ComponentModel.DataAnnotations;

namespace Frontend.Services
{
    public class AccountService
    {
        private readonly HttpClient _http;
        public AccountService(HttpClient http) 
        {
            _http = http;
        }
        public async Task<UserResponseDTO> CreateUserAsync(UserDTO user)
        {
            var response = await _http.PostAsJsonAsync("https://localhost:7290/api/User/signup", user);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserResponseDTO>();
                return result ?? new UserResponseDTO { message = "Empty response", status = false };
            }

            return new UserResponseDTO
            {
                message = "Failed to create user",
                status = false
            };
        }
        public async Task<UserResponseDTO> LoginUserAsync(LoginDTO user)
        {
            var response = await _http.PostAsJsonAsync("api/User/login", user);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserResponseDTO>();
                return result ?? new UserResponseDTO { message = "Empty response", status = false };
            }

            return new UserResponseDTO
            {
                message = "Login failed",
                status = false
            };
        }

    }
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }

    public class UserDTO 
    {
        public string Name {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserResponseDTO 
    {
        public string message { get; set; }
        public bool status { get; set; }
    }
}
