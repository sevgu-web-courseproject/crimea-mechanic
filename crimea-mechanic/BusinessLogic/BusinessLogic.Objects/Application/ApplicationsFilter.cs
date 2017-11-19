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
    }
}
