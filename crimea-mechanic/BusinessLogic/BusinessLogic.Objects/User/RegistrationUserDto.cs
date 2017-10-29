namespace BusinessLogic.Objects.User
{
    /// <summary>
    /// ДТО регистарации обычного пользователя
    /// </summary>
    public class RegistrationUserDto : RegistrationDto
    {
        /// <summary>
        /// Логин пользователя, также является email-адресом
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        public string PasswordСonfirmation { get; set; }

        /// <summary>
        /// Контактное имя пользователя
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Мобильный номер телефона пользователя
        /// </summary>
        public string Phone { get; set; }
    }
}
