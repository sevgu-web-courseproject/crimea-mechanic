namespace BusinessLogic.Objects.Review
{
    /// <summary>
    /// ДТО добавления нового отзыва
    /// </summary>
    public class AddReviewDto
    {
        /// <summary>
        /// Идентификатор автосервиса
        /// </summary>
        public long ServiceId { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public byte Mark { get; set; }

        /// <summary>
        /// Отзыв
        /// </summary>
        public string Review { get; set; }
    }
}
