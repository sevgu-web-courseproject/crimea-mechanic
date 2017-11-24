namespace DataAccessLayer.Models
{
    /// <summary>
    /// Отзыв об автосервисе
    /// </summary>
    public class CarServiceReview : DeletableBaseEntity<long>
    {
        /// <summary>
        /// Сервис
        /// </summary>
        public virtual CarService Service { get; set; }

        /// <summary>
        /// Автор отзыва
        /// </summary>
        public virtual UserProfile User { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public byte Mark { get ; set; }

        /// <summary>
        /// Отзыв
        /// </summary>
        public string Review { get; set; }
    }
}
