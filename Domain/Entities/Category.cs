namespace Domain.Entities
{
    /// <summary>
    /// Category entity for grouping menu items
    /// </summary>
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        // Navigation properties
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
