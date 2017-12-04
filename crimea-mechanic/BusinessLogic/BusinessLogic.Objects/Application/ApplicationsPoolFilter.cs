using System;

namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// Фильтр для заявок находящихся в пуле
    /// </summary>
    public class ApplicationsPoolFilter : BaseFilter
    {
        /// <summary>
        /// Созданные с
        /// </summary>
        public DateTime? CreatedFrom { get; set; }

        /// <summary>
        /// Созданные по
        /// </summary>
        public DateTime? CreatedTo { get; set; }

        /// <summary>
        /// Идентификатор марки
        /// </summary>
        public long? MarkId { get; set; }

        /// <summary>
        /// Идентификатор типа работы
        /// </summary>
        public long? WorkTypeId { get; set; }
    }
}
