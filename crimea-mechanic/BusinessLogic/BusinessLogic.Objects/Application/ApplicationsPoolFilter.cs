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
    }
}
