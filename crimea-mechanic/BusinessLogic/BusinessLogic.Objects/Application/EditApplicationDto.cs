namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// ДТО редактирования заявки
    /// </summary>
    public class EditApplicationDto
    {
        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        public long ApplicationId { get; set; }

        /// <summary>
        /// Новое описание
        /// </summary>
        public string Description { get; set; }
    }
}
