using System.Collections.Generic;

namespace BusinessLogic.Objects.Works
{
    /// <summary>
    /// ДТО класса работ
    /// </summary>
    public class WorkClassDto
    {
        /// <summary>
        /// Идентификатор класса работ
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименнование класса работ
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список типов работ
        /// </summary>
        public IEnumerable<WorkTypeDto> Types { get; set; }
    }
}
