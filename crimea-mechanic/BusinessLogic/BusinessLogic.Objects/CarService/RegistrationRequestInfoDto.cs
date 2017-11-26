using System.Collections.Generic;

namespace BusinessLogic.Objects.CarService
{
    /// <summary>
    /// ДТО заявки на регистрацию
    /// </summary>
    public class RegistrationRequestInfoDto : RegistrationRequestShortInfoDto
    {
        /// <summary>
        /// Об автосервисе
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Список фотографий
        /// </summary>
        public IEnumerable<long> PhotosId { get; set; }
    }
}
