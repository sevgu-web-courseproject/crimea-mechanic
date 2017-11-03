
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
        /// Пользователь
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
