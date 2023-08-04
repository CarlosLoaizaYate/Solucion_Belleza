using Belleza.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis.CSharp;
using System.Configuration;

namespace Belleza.Pages
{
    public partial class ProductDetail
    {
        protected string Message = string.Empty;

        protected Api.Models.Product product { get; set; } = new Api.Models.Product();

        [Parameter]
        public string id { get; set; }

        [Inject]
        private IProductService productService { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

       
        protected async override Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(id))
            {

            }
            else
            {
                var productId = Guid.Parse(id);
                
                var apiProduct = await productService.GetProduct(productId);
                if (apiProduct != null)
                {
                    product = apiProduct;
                }
            }
        }

        protected void HandleFailedRequest()
        {
            Message = "Algo salio mal.";
        }

        protected void GoToProducts()
        {
            navigationManager.NavigateTo("/product");
        }

        protected async Task DeleteProduct()
        {
            if (!string.IsNullOrEmpty(id))
            {
                var productId = Guid.Parse(id);
                var result = await productService.DeleteProduct(productId);
                if (result)
                {
                    navigationManager.NavigateTo("/product");
                }
                else
                {
                    Message = "Algo salio mal.";
                }
            }
        }

        protected async void HandleValidRequest()
        {
            if (string.IsNullOrEmpty(id))
            {
                var result = await productService.CreateProduct(product);
                if (result != null)
                {
                    navigationManager.NavigateTo("/product");
                }
                else
                {
                    Message = "Algo salio mal.";
                }
            }
            else
            {
                var result = await productService.UpdateProduct(product);
                if (result)
                {
                    navigationManager.NavigateTo("/product");
                }
                else
                {
                    Message = "Algo salio mal.";
                }
            }
        }

    }
}
