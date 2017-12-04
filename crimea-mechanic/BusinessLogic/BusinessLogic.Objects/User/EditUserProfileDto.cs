namespace BusinessLogic.Objects.User
{
    public class EditUserProfileDto
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Мобильный номер пользователя
        /// </summary>
        public string Phone { get; set; }
    }
}
