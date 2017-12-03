namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// ДТО для создание новой заявки
    /// </summary>
    public class CreateApplicationDto
    {
        /// <summary>
        /// Идентификатор машины
        /// </summary>
        public long CarId { get; set; }

        /// <summary>
        /// Идентификатор города
        /// </summary>
        public long CityId { get; set; }

        /// <summary>
        /// Тип работы
        /// </summary>
        public long? WorkTypeId { get; set; }

        /// <summary>
        /// Описание необходимых работы
        /// </summary>
        public string Description { get; set; }
    }
}
