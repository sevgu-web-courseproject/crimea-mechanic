using System.Collections.Generic;

namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// ДТО информации о заявке для обычного пользователя
    /// </summary>
    public class ApplicationInfoForUserDto : ApplicationShortInfoForUserDto
    {
        /// <summary>
        /// Предложения
        /// </summary>
        public List<OfferInfoDto> Offers { get; set; }

        /// <summary>
        /// Идентификатор города
        /// </summary>
        public long CityId { get; set; }
    }
}
