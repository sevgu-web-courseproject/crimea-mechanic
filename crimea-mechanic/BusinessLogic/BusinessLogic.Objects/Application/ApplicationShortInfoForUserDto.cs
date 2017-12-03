using Common.Enums;

namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// ДТО укороченной информации о заявке для обычного пользователя
    /// </summary>
    public class ApplicationShortInfoForUserDto : ApplicationBaseInfoDto
    {
        /// <summary>
        /// Наименнование обслуживающего сервиса
        /// Поле пустое если еще не выбран автосервис
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Идентификатор автосервиса
        /// </summary>
        public long? ServiceId { get; set; }

        /// <summary>
        /// Состояние предложения
        /// </summary>
        public ApplicationState State { get; set; }

        /// <summary>
        /// Описание состояния предложения
        /// </summary>
        public string StateDescription { get; set; }

        /// <summary>
        /// Наименнование города
        /// </summary>
        public string CityName { get; set; }
    }
}
