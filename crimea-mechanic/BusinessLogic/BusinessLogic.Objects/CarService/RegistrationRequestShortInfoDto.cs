using System;

namespace BusinessLogic.Objects.CarService
{
    /// <summary>
    /// ДТО укороченной информации о заявке
    /// </summary>
    public class RegistrationRequestShortInfoDto
    {
        /// <summary>
        /// Идентификатор автосервиса
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование автосервиса
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Название города
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Адрес автосервиса
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Идентификатор логотипа
        /// </summary>
        public long? LogoPhotoId { get; set; }

        /// <summary>
        /// Дата создания автосервиса
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Имя менеджера
        /// </summary>
        public string ManagerName { get; set; }
    }
}
