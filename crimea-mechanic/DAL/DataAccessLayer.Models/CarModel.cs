﻿namespace DataAccessLayer.Models
{
    /// <summary>
    /// Модель автомобиля
    /// </summary>
    public class CarModel : DeletableBaseEntity<long>
    {
        /// <summary>
        /// Наименнование модели
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Марка автомобиля
        /// </summary>
        public virtual CarMark Mark { get; set; }
    }
}
