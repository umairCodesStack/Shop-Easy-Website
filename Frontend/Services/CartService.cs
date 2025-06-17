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
            var response = await _http.PostAsJsonAsync("https://localhost:7290/api/Cart/AddToCart", item);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to add item to cart");
            }
        }
    }

}
