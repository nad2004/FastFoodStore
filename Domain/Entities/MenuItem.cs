namespace Domain.Entities
{
    /// <summary>
    /// MenuItem entity representing a menu item available for ordering
    /// </summary>
    public class MenuItem : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; } = true;
        
        // Navigation properties
        public Category Category { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
