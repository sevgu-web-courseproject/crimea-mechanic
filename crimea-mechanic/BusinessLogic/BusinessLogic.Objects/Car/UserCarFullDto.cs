using System.Collections.Generic;

namespace BusinessLogic.Objects.Car
{
    public class UserCarFullDto : UserCarDto
    {
        /// <summary>
        /// Заявки
        /// </summary>
        public IEnumerable<ApplicationForCarHistoryDto> Applications { get; set; }
    }
}
