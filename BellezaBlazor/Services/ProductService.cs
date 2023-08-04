using Api.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace BellezaBlazor.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>?> GetProducts()
        {
            var apiResponse = await _httpClient.GetStreamAsync("/api/Product");
            //if (true)
            //{

            //}
            //var response = 
            var products = await JsonSerializer.DeserializeAsync<IEnumerable<Product>?>(apiResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return products;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/Product/{id}");
        }

        public async Task<Product> Update(Product product)
        {
            await _httpClient.PutAsJsonAsync($"{product.ProductId}", product);
            return product;
        }
    }
    public interface IProductService
    {
        Task<IEnumerable<Product>?> GetProducts();
        Task<Product> GetProduct(int id);
        Task<Product> Update(Product product);

    }
}
