using System.Collections.Generic;
using BusinessLogic.Objects.Works;

namespace BusinessLogic.Objects.CarService
{
    /// <summary>
    /// ДТО заявки на регистрацию
    /// </summary>
    public class RegistrationRequestInfoDto : RegistrationRequestShortInfoDto
    {
        /// <summary>
        /// Сайт автосервиса
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Email автосервиса
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// График работ
        /// </summary>
        public string TimetableWorks { get; set; }

        /// <summary>
        /// Телефоны автосервиса
        /// </summary>
        public IEnumerable<string> Phones { get; set; }

        /// <summary>
        /// Об автосервисе
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Список фотографий
        /// </summary>
        public IEnumerable<long> PhotosId { get; set; }

        /// <summary>
        /// Классы работ
        /// </summary>
        public IEnumerable<WorkClassDto> WorkClasses { get; set; }
    }
}
