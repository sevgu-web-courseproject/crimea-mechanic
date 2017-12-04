using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    /// <summary>
    /// Классы работ
    /// </summary>
    public class WorkClass : DeletableBaseEntity<long>
    {
        /// <summary>
        /// Наименнование класса
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Типы работ
        /// </summary>
        public virtual ICollection<WorkType> WorkTypes { get; set; }
    }
}
