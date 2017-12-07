using System.Collections.Generic;
using Common.Enums;

namespace DataAccessLayer.Models
{
    /// <summary>
    /// Заявка на обслуживание автомобиля
    /// </summary>
    public class Application : DeletableBaseEntity<long>
    {
        /// <summary>
        /// Обслуживаемая машина
        /// </summary>
        public virtual UserCar Car { get; set; }

        /// <summary>
        /// Сервис, который будет исполнять заявку
        /// </summary>
        public virtual CarService Service { get; set; }

        /// <summary>
        /// Типы работ
        /// </summary>
        public virtual ICollection<WorkType> WorkTypes { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public virtual City City { get; set; }

        /// <summary>
        /// Предложения
        /// </summary>
        public virtual ICollection<ApplicationOffer> Offers { get; set; }

        /// <summary>
        /// Состояние заявки
        /// </summary>
        public ApplicationState State { get; set; }

        /// <summary>
        /// Описание необходимых работ
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Принадлежит ли заявка автосервису
        /// </summary>
        public bool IsBelongToService(string userId)
        {
            return Service != null && Service.ApplicationUser.Id == userId;
        }
    }
}
