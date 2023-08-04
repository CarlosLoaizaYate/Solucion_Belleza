using Api.Context;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class ProductService : IProductService
    {
        readonly BellezaContext context;

        public ProductService(BellezaContext dbContext)
        {
            context = dbContext;
        }

        public IEnumerable<Product> Get()
        {
            return context.products;
        }

        public Product? GetId(Guid id)
        {
            var productCurrent = context.products.Find(id);
            return productCurrent;
        }

        public void Save(Product product)
        {
            product.ProductId = new Guid();
            product.CreationDate = DateTime.Now;
            context.Add(product);
            context.SaveChanges();
        }

        public void Update(Product product, Guid id)
        {
            var productCurrent = context.products.Find(id);

            if (productCurrent != null)
            {
                productCurrent.Name = product.Name;
                productCurrent.Description = product.Description;

                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var productCurrent = context.products.Find(id);
            if (productCurrent != null)
            {
                context.Remove(productCurrent);
                context.SaveChanges();
            }
        }


    }

    public interface IProductService
    {
        IEnumerable<Product> Get();
        Product? GetId(Guid id);
        void Save(Product categoria);
        void Update(Product categoria, Guid id);
        void Delete(Guid id);
    }
}
