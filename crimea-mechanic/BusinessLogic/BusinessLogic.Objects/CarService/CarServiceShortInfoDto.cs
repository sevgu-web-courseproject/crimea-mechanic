using System;

namespace BusinessLogic.Objects.CarService
{
    /// <summary>
    /// ДТО укороченной информации об автосервисе
    /// </summary>
    public class CarServiceShortInfoDto
    {
        /// <summary>
        /// Идентификатор автосервиса
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименнование автосервиса
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Средняя оценка
        /// </summary>
        public long AverageMark { get; set; }

        /// <summary>
        /// Наименнования города
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Адрес местоположения
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Идентификатор фотографии логотипа автосервиса
        /// </summary>
        public long LogoPhotoId { get; set; }

        /// <summary>
        /// Дата создания автосервиса
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Текстовое поле "Об автосервисе"
        /// </summary>
        public string About { get; set; }
    }
}
