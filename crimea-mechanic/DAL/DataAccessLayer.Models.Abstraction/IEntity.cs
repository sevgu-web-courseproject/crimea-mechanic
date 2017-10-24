namespace DataAccessLayer.Models.Abstraction
{
    /// <summary>
    /// Base entity
    /// </summary>
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
