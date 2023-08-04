using Api.Models;
using System.Text;
using System.Text.Json;

namespace Belleza.Services
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
            try
            {
                var apiResponse = await _httpClient.GetStreamAsync($"https://localhost:7240/api/Product");
                var products = await JsonSerializer.DeserializeAsync<IEnumerable<Product>?>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetProducts: {ex.Message}");
                return null;
            }

        }

        public async Task<Product?> GetProduct(Guid id)
        {
            try
            {
                var apiResponse = await _httpClient.GetStreamAsync($"https://localhost:7240/api/Product/{id}");
                var product = await JsonSerializer.DeserializeAsync<Product?>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetProduct: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateProduct(Product product)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"https://localhost:7240/api/Product", itemJson);
                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CreateProduct: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://localhost:7240/api/Product/{product.ProductId}", itemJson);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateProduct: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7240/api/Product/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en DeleteProduct: {ex.Message}");
                return false;
            }
        }
    }
    public interface IProductService
    {
        Task<IEnumerable<Product>?> GetProducts();
        Task<Product?> GetProduct(Guid id);
        Task<bool> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid id);
    }
}
