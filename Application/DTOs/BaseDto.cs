namespace Application.DTOs
{
    /// <summary>
    /// Base DTO class for all data transfer objects
    /// </summary>
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
