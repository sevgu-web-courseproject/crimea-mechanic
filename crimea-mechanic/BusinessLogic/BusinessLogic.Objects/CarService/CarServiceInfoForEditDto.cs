using System.Collections.Generic;

namespace BusinessLogic.Objects.CarService
{
    /// <summary>
    /// ДТО информации для редактирования автосервиса
    /// </summary>
    public class CarServiceInfoForEditDto
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
        /// Имя менеджера автосервиса
        /// </summary>
        public string ManagerName { get; set; }

        /// <summary>
        /// Адрес автосервиса
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Email автосервиса
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Номера автосервиса
        /// </summary>
        public IEnumerable<string> Phones { get; set; }

        /// <summary>
        /// Сайт автосервиса
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// График работы автосервиса
        /// </summary>
        public string TimetableWorks { get; set; }
        
        /// <summary>
        /// Об автосервисе
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Типы работ
        /// </summary>
        public IEnumerable<long> WorkTypes { get; set; }

        /// <summary>
        /// Идентификатор логотипа
        /// </summary>
        public long? LogoId { get; set; }

        /// <summary>
        /// Фотографии автосервиса
        /// </summary>
        public IEnumerable<long> Photos { get; set; }
    }
}
