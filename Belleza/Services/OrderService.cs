using Api.Models;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace Belleza.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Order>?> GetOrders()
        {
            try
            {
                var apiResponse = await _httpClient.GetStreamAsync($"https://localhost:7240/api/Order");
                var order = await JsonSerializer.DeserializeAsync<IEnumerable<Order>?>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return order;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetProducts: {ex.Message}");
                return null;
            }

        }

        public async Task<Order?> GetOrder(Guid id)
        {
            try
            {
                var apiResponse = await _httpClient.GetStreamAsync($"https://localhost:7240/api/Order/{id}");
                var order = await JsonSerializer.DeserializeAsync<Order?>(apiResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return order;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetProduct: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateOrder(Order order)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"https://localhost:7240/api/Order", itemJson);
                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CreateProduct: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://localhost:7240/api/Order/{order.OrderId}", itemJson);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateProduct: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteOrder(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7240/api/Order/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en DeleteProduct: {ex.Message}");
                return false;
            }
        }
    }
    public interface IOrderService
    {
        Task<IEnumerable<Order>?> GetOrders();
        Task<Order?> GetOrder(Guid id);
        Task<bool> CreateOrder(Order product);
        Task<bool> UpdateOrder(Order product);
        Task<bool> DeleteOrder(Guid id);
    }
}
