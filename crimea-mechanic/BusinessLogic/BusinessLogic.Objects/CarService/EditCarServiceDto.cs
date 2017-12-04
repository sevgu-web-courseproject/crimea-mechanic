﻿using System.Collections.Generic;

namespace BusinessLogic.Objects.CarService
{
    /// <summary>
    /// ДТО редактирования автосервиса
    /// </summary>
    public class EditCarServiceDto
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
        /// Адрес автосервиса
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Контактный email автосервиса
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Контактные мобильные номера автосервиса
        /// </summary>
        public List<string> Phones { get; set; }

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
        /// Теги типов работ
        /// </summary>
        public List<long> WorkTypes { get; set; }

        /// <summary>
        /// Логотип автосервиса
        /// </summary>
        public FileDto Logo { get; set; }

        /// <summary>
        /// Фотографии автосервиса
        /// </summary>
        public List<FileDto> Photos { get; set; }

        /// <summary>
        /// Текстовое поле "Об автосервисе"
        /// </summary>
        public string About { get; set; }
    }
}
