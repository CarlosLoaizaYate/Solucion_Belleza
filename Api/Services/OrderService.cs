using Api.Context;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class OrderService : IOrderService
    {
        readonly BellezaContext context;

        public OrderService(BellezaContext dbContext)
        {
            context = dbContext;
        }

        public IEnumerable<Order> Get()
        {
            return context.orders.Include(x => x.Product);
        }

        public Order? GetId(Guid id)
        {
            IEnumerable<Order> orderCurrent = context.orders.Include(x => x.Product).Where(x => x.OrderId == id);
            return orderCurrent.FirstOrDefault();
        }

        public void Save(Order order)
        {
            order.OrderId = new Guid();
            order.CreationDate = DateTime.Now;
            context.Add(order);
            context.SaveChanges();
        }

        public void Update(Order order, Guid id)
        {
            var orderCurrent = context.orders.Find(id);

            if (orderCurrent != null)
            {
                orderCurrent.ProducId = order.ProducId;
                orderCurrent.Quantity = order.Quantity;

                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var orderCurrent = context.orders.Find(id);
            if (orderCurrent != null)
            {
                context.Remove(orderCurrent);
                context.SaveChanges();
            }

        }
    }
    public interface IOrderService
    {
        IEnumerable<Order> Get();
        Order? GetId(Guid id);
        void Save(Order categoria);
        void Delete(Guid id);
        void Update(Order categoria, Guid id);
    }
}
