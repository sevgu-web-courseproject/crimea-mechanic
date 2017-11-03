using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    /// <summary>
    /// Тег марки автомобиля
    /// </summary>
    public class CarModelTag : DeletableBaseEntity<long>
    {
        /// <summary>
        /// Наименнование тега
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Автосервис использующий данный тег
        /// </summary>
        public virtual ICollection<CarService> CarServices { get; set; }
    }
}
