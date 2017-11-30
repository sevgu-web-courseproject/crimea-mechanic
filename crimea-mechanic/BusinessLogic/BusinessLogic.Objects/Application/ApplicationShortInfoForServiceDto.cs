namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// ДТО укороченной информации о заявке
    /// </summary>
    public class ApplicationShortInfoForServiceDto : ApplicationBaseInfoDto
    {
        /// <summary>
        /// Контактное имя автора заявки
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Идентификатор предложения если оно отправлено
        /// </summary>
        public long? OfferId { get; set; }
    }
}
