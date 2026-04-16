namespace Domain.Entities
{
    /// <summary>
    /// Order entity representing a customer order
    /// </summary>
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // Pending, Preparing, Ready, Delivered, Cancelled
        public DateTime OrderDate { get; set; }
        public string PaymentMethod { get; set; }
        public string DeliveryAddress { get; set; }
        
        // Navigation properties
        public Customer Customer { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
