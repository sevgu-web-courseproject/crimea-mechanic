using System;
using Common.Enums;

namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// Фильтр для отображению заявок пользователю
    /// </summary>
    public class ApplicationsFilter : BaseFilter
    {
        /// <summary>
        /// Состояние заявки
        /// </summary>
        public ApplicationState? State { get; set; }

        /// <summary>
        /// Созданные с
        /// </summary>
        public DateTime? CreatedFrom { get; set; }

        /// <summary>
        /// Созданные до
        /// </summary>
        public DateTime? CreatedTo { get; set; }
    }
}
