namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// ДТО инфомрации о предложении
    /// </summary>
    public class OfferInfoDto
    {
        /// <summary>
        /// Идентификатор предложения
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименнование автосервиса
        /// </summary>
        public string ServiceName { get; set; }
        
        /// <summary>
        /// Идентификатор автосервиса
        /// </summary>
        public long ServiceId { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Содержание предложения
        /// </summary>
        public string Сontent { get; set; }
    }
}
