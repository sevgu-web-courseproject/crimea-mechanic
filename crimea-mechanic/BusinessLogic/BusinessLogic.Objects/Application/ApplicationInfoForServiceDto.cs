using Common.Enums;

namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// ДТО информация о заявке для автосервиса
    /// </summary>
    public class ApplicationInfoForServiceDto : ApplicationBaseInfoDto
    {
        /// <summary>
        /// Контактное имя автора заявки
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Контактный номер телефона автора заявки
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Состояние предложения
        /// </summary>
        public ApplicationState State { get; set; }

        /// <summary>
        /// Описание состояния предложения
        /// </summary>
        public string StateDescription { get; set; }
    }
}
