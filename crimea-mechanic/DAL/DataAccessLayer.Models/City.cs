using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    /// <summary>
    /// Город
    /// </summary>
    public class City : DeletableBaseEntity<long>
    {
        /// <summary>
        /// Наименнование города
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Автосервисы находящиеся в городе
        /// </summary>
        public virtual ICollection<CarService> CarServices { get; set; }
    }
}
