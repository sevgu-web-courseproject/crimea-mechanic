using System;
using System.Collections.Generic;
using BusinessLogic.Objects.Car;
using BusinessLogic.Objects.Works;

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
        /// Классы работ
        /// </summary>
        public IEnumerable<WorkClassDto> WorkClasses { get; set; }
        
        /// <summary>
        /// Описание заявки
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата и время создания завяки
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Признак удалённой заявки
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
