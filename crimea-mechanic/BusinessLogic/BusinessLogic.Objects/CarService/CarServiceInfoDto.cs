using System.Collections.Generic;
using BusinessLogic.Objects.Review;

namespace BusinessLogic.Objects.CarService
{
    /// <summary>
    /// ДТО информации об автосервисе
    /// </summary>
    public class CarServiceInfoDto : CarServiceShortInfoDto
    {
        /// <summary>
        /// Отзывы
        /// </summary>
        public List<ReviewInfoDto> Reviews { get; set; }
        
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Номера телефонов
        /// </summary>
        public List<string> Phones { get; set; }

        /// <summary>
        /// Имя менеджера
        /// </summary>
        public string ManagerName { get; set; }

        /// <summary>
        /// Сайт
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// График работы автосервиса
        /// </summary>
        public string TimetableWorks { get; set; }

        /// <summary>
        /// Идентификаторы фотографии автосервиса
        /// </summary>
        public List<long> PhotosId { get; set; }
    }
}
