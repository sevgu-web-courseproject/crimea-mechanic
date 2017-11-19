namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// ДТО добавления предложения для заявки
    /// </summary>
    public class AddOfferDto
    {
        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        public long ApplicationId { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Содержание заявки
        /// </summary>
        public string Content { get; set; }
    }
}
