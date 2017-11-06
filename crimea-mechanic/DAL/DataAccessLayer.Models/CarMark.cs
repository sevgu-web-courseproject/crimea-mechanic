using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    /// <summary>
    /// Марка автомобиля
    /// </summary>
    public class CarMark : DeletableBaseEntity<long>
    {
        /// <summary>
        /// Наименнование марки
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Модели марки
        /// </summary>
        public virtual ICollection<CarModel> Models { get; set; }

        /// <summary>
        /// Автосервисы работающие с данной маркой автомобиля
        /// </summary>
        public virtual ICollection<CarService> CarServices { get; set; }
    }
}
