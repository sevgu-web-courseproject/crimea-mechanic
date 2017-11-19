namespace DataAccessLayer.Models
{
    /// <summary>
    /// Предложения заявки
    /// </summary>
    public class ApplicationOffer : DeletableBaseEntity<long>
    {
        /// <summary>
        /// Заявка
        /// </summary>
        public virtual Application Application { get; set; }

        /// <summary>
        /// Владелец предложения
        /// </summary>
        public virtual CarService Service { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Содержание предложения
        /// </summary>
        public string Content { get; set; }
    }
}
