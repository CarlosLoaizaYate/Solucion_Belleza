using Belleza.Services;
using Microsoft.AspNetCore.Components;
using System.Configuration;

namespace Belleza.Pages
{
    public partial class Order
    {
        [Inject]
        private IOrderService orderService { get; set; }

        public IEnumerable<Api.Models.Order> _orders { get; set; } = new List<Api.Models.Order>();

        protected async override Task OnInitializedAsync()
        {
            var apiOrders = await orderService.GetOrders();
            if (apiOrders.Any() && apiOrders != null)
            {
                _orders = apiOrders;
            }
        }
    }
}
