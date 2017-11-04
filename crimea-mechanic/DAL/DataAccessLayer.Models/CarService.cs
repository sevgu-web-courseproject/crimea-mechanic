using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;

namespace DataAccessLayer.Models
{
    /// <summary>
    /// Модель автосервиса
    /// </summary>
    public class CarService : BaseEntity<long>
    {
        /// <summary>
        /// Наименнование автосервиса
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Адрес автосервиса
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Контактный email автосервиса
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Мобильные номера автосервиса
        /// </summary>
        public virtual ICollection<CarServicePhone> Phones { get; set; }

        /// <summary>
        /// Контактное имя менеджера автосервиса
        /// </summary>
        public string ManagerName { get; set; }

        /// <summary>
        /// Ссылка на сайт/паблик автосервиса
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// График работы автосервиса
        /// </summary>
        public string TimetableWorks { get; set; }

        /// <summary>
        /// Теги характера работ
        /// </summary>
        public virtual ICollection<WorkTag> WorkTags { get; set; }

        /// <summary>
        /// Теги марок автомобилей
        /// </summary>
        public virtual ICollection<CarModelTag> CarModelTags { get; set; }

        /// <summary>
        /// Файлы автосервиса
        /// </summary>
        public virtual ICollection<CarServiceFile> Files { get; set; }

        /// <summary>
        /// Текстовое поле "Об автосервисе"
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Дата изменения данных об автосервисе
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Состояние автосервиса
        /// </summary>
        public CarServiceState State { get; set; }
    }
}
