using System.Collections.Generic;

namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// ДТО для создание новой заявки
    /// </summary>
    public class CreateApplicationDto
    {
        /// <summary>
        /// Идентификатор машины
        /// </summary>
        public long CarId { get; set; }

        /// <summary>
        /// Идентификатор города
        /// </summary>
        public long CityId { get; set; }

        /// <summary>
        /// Типы работы
        /// </summary>
        public IEnumerable<long> WorkTypes { get; set; }

        /// <summary>
        /// Описание необходимых работы
        /// </summary>
        public string Description { get; set; }
    }
}
