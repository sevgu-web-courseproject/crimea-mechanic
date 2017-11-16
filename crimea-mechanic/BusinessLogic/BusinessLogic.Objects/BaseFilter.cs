namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// Базовый фильтр
    /// </summary>
    public abstract class BaseFilter
    {
        /// <summary>
        /// Элементов на странице
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Текущий номер страницы
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
