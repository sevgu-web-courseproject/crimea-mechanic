﻿using System;
using System.Collections.Generic;
using Common.Enums;

namespace DataAccessLayer.Models
{
    public class UserProfile : BaseEntity<long>
    {
        /// <summary>
        /// Контактное имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер мобильного телефона
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Дата последнего обновления модели
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Состояние пользователя
        /// </summary>
        public UserState State { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Машины пользователя
        /// </summary>
        public virtual ICollection<UserCar> Cars { get; set; }

        /// <summary>
        /// Отзывы
        /// </summary>
        public virtual ICollection<CarServiceReview> Reviews { get; set; }
    }
}
