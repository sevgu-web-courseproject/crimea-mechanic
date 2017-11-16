using Common.Enums;

namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// ДТО укороченной информации о заявке для администратора
    /// </summary>
    public class ApplicationShortInfoForAdministratorDto : ApplicationBaseInfoDto
    {
        /// <summary>
        /// Идентификатор профиля пользователя
        /// </summary>
        public long UserProfileId { get; set; }

        /// <summary>
        /// Контактное имя автора заявки
        /// </summary>
        public string UserContactName { get; set; }
        
        /// <summary>
        /// Идентификатор автосервиса
        /// </summary>
        public long? CarServiceId { get; set; }

        /// <summary>
        /// Наименнование автосервиса
        /// </summary>
        public string CarServiceName { get; set; }

        /// <summary>
        /// Наименнование города
        /// </summary>
        public string CityName { get; set; }

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
