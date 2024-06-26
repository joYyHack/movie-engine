namespace Movies.DAL.Entities
{
    /// <summary>
    /// Represents the base entity with common properties.
    /// </summary>
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
