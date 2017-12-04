using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    /// <summary>
    /// Тип работы
    /// </summary>
    public class WorkType : DeletableBaseEntity<long>
    {
        /// <summary>
        /// Наименнование типа работы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Класс работ
        /// </summary>
        public virtual WorkClass Class { get; set; }

        /// <summary>
        /// Автосервисы с данными типом работ
        /// </summary>
        public virtual ICollection<CarService> CarServices { get; set; }
        
        /// <summary>
        /// Заявки с данными типом
        /// </summary>
        public virtual ICollection<Application> Applications { get; set; }
    }
}
