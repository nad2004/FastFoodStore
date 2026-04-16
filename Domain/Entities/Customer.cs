namespace Domain.Entities
{
    /// <summary>
    /// Customer entity representing a customer in the system
    /// </summary>
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        
        // Navigation properties
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
