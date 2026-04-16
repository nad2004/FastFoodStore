namespace Domain.Entities
{
    /// <summary>
    /// OrderItem entity representing a single item in an order
    /// </summary>
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        
        // Navigation properties
        public Order Order { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}
