using System;

namespace BusinessLogic.Objects.User
{
    public class UserProfileDto
    {
        /// <summary>
        /// Идентификатор профайла пользователя
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Email адрес
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Мобильный номер телефона
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime Created { get; set; }
    }
}
