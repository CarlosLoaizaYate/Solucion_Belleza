using Belleza.Services;
using Microsoft.AspNetCore.Components;
using System.Configuration;

namespace Belleza.Pages
{
    public partial class Product
    {
        [Inject]
        private IProductService productService { get; set; }

        public IEnumerable<Api.Models.Product> _products { get; set; } = new List<Api.Models.Product>();

        protected async override Task OnInitializedAsync()
        {
            var apiProducts = await productService.GetProducts();
            if (apiProducts.Any() && apiProducts != null)
            {
                _products = apiProducts;
            }
        }
    }
}
