namespace BusinessLogic.Objects.User
{
    /// <summary>
    /// ДТО регистарации пользователя
    /// </summary>
    public  class RegistrationDto
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
    }
}
