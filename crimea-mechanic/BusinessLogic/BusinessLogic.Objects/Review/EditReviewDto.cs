namespace BusinessLogic.Objects.Review
{
    /// <summary>
    /// ДТО редактирования отзыва
    /// </summary>
    public class EditReviewDto
    {
        /// <summary>
        /// Идентификатор отзыва
        /// </summary>
        public long ReviewId { get; set; }

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
