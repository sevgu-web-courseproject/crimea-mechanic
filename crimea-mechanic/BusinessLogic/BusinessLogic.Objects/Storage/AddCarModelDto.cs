namespace BusinessLogic.Objects.Storage
{
    /// <summary>
    /// ДТО добавления новой модели автомобиля
    /// </summary>
    public class AddCarModelDto
    {
        /// <summary>
        /// Марка автомобиля
        /// </summary>
        public long MarkId { get; set; }

        /// <summary>
        /// Наименнование модели
        /// </summary>
        public string Name { get; set; }
    }
}
