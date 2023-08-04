using Belleza.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis.CSharp;
using System.Configuration;

namespace Belleza.Pages
{
    public partial class OrderDetail
    {
        protected string Message = string.Empty;

        protected Api.Models.Order order { get; set; } = new Api.Models.Order();
        public IEnumerable<Api.Models.Product> products { get; set; } = new List<Api.Models.Product>();


        [Parameter]
        public string id { get; set; }

        public string orderProducId = string.Empty;

        [Inject]
        private IOrderService orderService { get; set; }
        [Inject]
        private IProductService productService { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var apiProducts = await productService.GetProducts();
            if (apiProducts.Any() && apiProducts != null)
            {
                products = apiProducts;
            }

            if (string.IsNullOrEmpty(id))
            {
                orderProducId = "";
            }
            else
            {
                var orderId = Guid.Parse(id);
                var apiOrder = await orderService.GetOrder(orderId);

                if (apiOrder != null)
                {
                    orderProducId = apiOrder.ProducId.ToString();
                    order = apiOrder;
                }
            }
        }

        protected void HandleFailedRequest()
        {
            Message = "Algo salio mal.";
        }

        protected void GoToProducts()
        {
            navigationManager.NavigateTo("/order");
        }

        protected async Task DeleteProduct()
        {
            if (!string.IsNullOrEmpty(id))
            {
                var orderId = Guid.Parse(id);
                var result = await orderService.DeleteOrder(orderId);
                if (result)
                {
                    navigationManager.NavigateTo("/order");
                }
                else
                {
                    Message = "Algo salio mal.";
                }
            }
        }

        protected async void HandleValidRequest()
        {
            order.ProducId = Guid.Parse(orderProducId);

            if (string.IsNullOrEmpty(id))
            {
                var result = await orderService.CreateOrder(order);
                if (result != null)
                {
                    navigationManager.NavigateTo("/order");
                }
                else
                {
                    Message = "Algo salio mal.";
                }
            }
            else
            {
                var result = await orderService.UpdateOrder(order);
                if (result)
                {
                    navigationManager.NavigateTo("/order");
                }
                else
                {
                    Message = "Algo salio mal.";
                }
            }
        }

    }
}
