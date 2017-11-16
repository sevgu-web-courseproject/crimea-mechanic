using System.Collections.Generic;

namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// ДТО информации о заявке для администратора
    /// </summary>
    public class ApplicationInfoForAdministratorDto : ApplicationShortInfoForAdministratorDto
    {
        /// <summary>
        /// Предложения
        /// </summary>
        public List<OfferInfoDto> Offers { get; set; }
    }
}
