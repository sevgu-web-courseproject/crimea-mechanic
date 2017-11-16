using System;
using BusinessLogic.Objects.Car;

namespace BusinessLogic.Objects.Application
{
    /// <summary>
    /// ДТО базовой для всех информации о заявке
    /// </summary>
    public abstract class ApplicationBaseInfoDto
    {
        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Машина
        /// </summary>
        public UserCarDto Car { get; set; }

        /// <summary>
        /// Описание заявки
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата и время создания завяки
        /// </summary>
        public DateTime Created { get; set; }
    }
}
