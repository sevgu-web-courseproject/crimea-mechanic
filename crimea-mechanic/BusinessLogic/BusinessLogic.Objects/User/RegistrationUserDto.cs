namespace BusinessLogic.Objects.User
{
    /// <summary>
    /// ДТО регистарации обычного пользователя
    /// </summary>
    public class RegistrationUserDto : RegistrationDto
    {
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
