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
        /// Признак - предложение уже отправлено?
        /// </summary>
        public bool IsOfferSended { get; set; }

        /// <summary>
        /// Признак - предложение принято?
        /// </summary>
        public bool IsOfferAccepted { get; set; }
    }
}
