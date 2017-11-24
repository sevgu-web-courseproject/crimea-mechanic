namespace BusinessLogic.Objects.Review
{
    /// <summary>
    /// ДТО редактирования отзыва
    /// </summary>
    public class EditReviewDto : OperateReviewDto
    {
        /// <summary>
        /// Идентификатор отзыва
        /// </summary>
        public long ReviewId { get; set; }
    }
}
