using Common.Enums;

namespace BusinessLogic.Objects.User
{
    /// <summary>
    /// ДТО регистарации пользователя
    /// </summary>
    public abstract class RegistrationDto
    {
        /// <summary>
        /// Логин пользователя
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
        /// Тип регистрации
        /// </summary>
        public RegistrationTypeEnum Type { get; set; }
    }
}
