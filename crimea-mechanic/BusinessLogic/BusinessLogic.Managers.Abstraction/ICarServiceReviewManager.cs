using BusinessLogic.Objects.Review;

namespace BusinessLogic.Managers.Abstraction
{
    public interface ICarServiceReviewManager
    {
        /// <summary>
        /// Добавить новый отзыв
        /// </summary>
        void Add(AddReviewDto dto, string currentUserId);

        /// <summary>
        /// Отредактировать отзыв
        /// </summary>
        void Edit(EditReviewDto dto, string currentUserId);

        /// <summary>
        /// Удалить отзыв
        /// </summary>
        void Delete(long reviewId, string currentUserId);
    }
}
