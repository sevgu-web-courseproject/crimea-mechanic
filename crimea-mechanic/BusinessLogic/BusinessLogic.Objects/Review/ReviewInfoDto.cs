using System;

namespace BusinessLogic.Objects.Review
{
    /// <summary>
    /// ДТО отображния отзыва
    /// </summary>
    public class ReviewInfoDto
    {
        /// <summary>
        /// Идентификатор отзыва
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public byte Mark { get; set; }

        /// <summary>
        /// Отзыв
        /// </summary>
        public string Review { get; set; }
        
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public string Author { get; set; }
    }
}
