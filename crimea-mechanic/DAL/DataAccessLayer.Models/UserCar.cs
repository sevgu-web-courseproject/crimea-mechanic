using System.Collections.Generic;
using Common.Enums;

namespace DataAccessLayer.Models
{
    /// <summary>
    /// Машина пользователя
    /// </summary>
    public class UserCar : DeletableBaseEntity<long>
    {
        /// <summary>
        /// Наименнование машины (пользовательское)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Модель машины
        /// </summary>
        public virtual CarModel Model { get; set; }

        /// <summary>
        /// Заявки автомобиля
        /// </summary>
        public virtual ICollection<Application> Applications { get; set; }

        /// <summary>
        /// Год выпуска автомобиля
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// VIN номер автомобиля
        /// </summary>
        public string Vin { get; set; }

        /// <summary>
        /// Тип топлива
        /// </summary>
        public FuelType FuelType { get; set; }

        /// <summary>
        /// Объем двигателя
        /// </summary>
        public float EngineCapacity { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual UserProfile User { get; set; }
    }
}
