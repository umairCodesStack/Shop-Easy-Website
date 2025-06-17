using Domain.DTOs;

namespace Frontend.Services
{
    public class ProductService
    {
        private readonly HttpClient _http;
        public ProductService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<GetProductDTO>> GetProductsAsync()
        {
            var response = await _http.GetAsync("https://localhost:7290/odata/Product");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<GetProductDTO>>();
                return result ?? new List<GetProductDTO>();
            }
            return new List<GetProductDTO>();
        }

        public async Task<GetProductDTO> GetProductByIdAsync(int id)
        {
            var response = await _http.GetAsync($"https://localhost:7290/api/Product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetProductDTO>();
                return result ?? new GetProductDTO();
            }
            return new GetProductDTO();
        }
    }
}
