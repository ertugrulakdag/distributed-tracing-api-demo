using DTD.Model;
using System.Net.Http.Json;

namespace DTD.Core.Clients
{
    public class ShopClient
    {
        private readonly HttpClient _httpClient;

        public ShopClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDto?> GetProductAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ProductDto>($"/Product/GetProductById/{id}");
            return response;
        }
    }
}
