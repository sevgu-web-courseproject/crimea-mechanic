using Common.Enums;

namespace BusinessLogic.Objects.User
{
    /// <summary>
    /// ДТО регистарации пользователя
    /// </summary>
    public abstract class RegistrationDto
    {
        /// <summary>
        /// Тип регистрации
        /// </summary>
        public RegistrationTypeEnum Type { get; set; }
    }
}
