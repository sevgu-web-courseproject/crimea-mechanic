
namespace DataAccessLayer.Models
{
    /// <summary>
    /// Контактный мобильный номер автосервиса
    /// </summary>
    public class CarServicePhone : DeletableBaseEntity<long>
    {
        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Автосервис
        /// </summary>
        public virtual CarService CarService { get; set; }
    }
}
