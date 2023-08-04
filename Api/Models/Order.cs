namespace Api.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid ProducId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Product Product { get; set; }
    }
}
