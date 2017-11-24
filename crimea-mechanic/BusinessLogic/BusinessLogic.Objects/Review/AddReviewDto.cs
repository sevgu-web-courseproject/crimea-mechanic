namespace BusinessLogic.Objects.Review
{
    /// <summary>
    /// ДТО добавления нового отзыва
    /// </summary>
    public class AddReviewDto : OperateReviewDto
    {
        /// <summary>
        /// Идентификатор автосервиса
        /// </summary>
        public long ServiceId { get; set; }
    }
}
