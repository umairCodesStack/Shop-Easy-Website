using Blazored.SessionStorage;
using Domain.DTOs;

namespace Frontend.Services
{
    public class CartService
    {
        private readonly HttpClient _http;
        private readonly ISessionStorageService _session;

        public CartService(HttpClient http, ISessionStorageService session)
        {
            _http = http;
            _session = session;
        }

        public async Task AddToCartAsync(AddToCartDTO item)
        {
            // Retrieve userId from session storage
            var userId = await _session.GetItemAsync<string?>("UserId");

            if (userId == null)
            {
                throw new Exception("User not logged in or session expired.");
            }

            item.userid = userId;

            var response = await _http.PostAsJsonAsync("https://localhost:7290/api/Cart/AddToCart", item);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to add item to cart");
            }
        }
    }

}
