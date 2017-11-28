using System;

namespace BusinessLogic.Objects.Car
{
    public class ApplicationForCarHistoryDto
    {
        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Описание заявки
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата заявки
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Описание состояния
        /// </summary>
        public string StateDescription { get; set; }
    }
}
