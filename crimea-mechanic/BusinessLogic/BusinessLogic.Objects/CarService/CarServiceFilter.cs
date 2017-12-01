using BusinessLogic.Objects.Application;

namespace BusinessLogic.Objects.CarService
{
    /// <summary>
    /// Фильтр автосервисов
    /// </summary>
    public class CarServiceFilter : BaseFilter
    {
        /// <summary>
        /// Наименнование автосервиса
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Идентификатор города
        /// </summary>
        public long? CityId { get; set; }

        /// <summary>
        /// Количество звезд
        /// </summary>
        public byte? Stars { get; set; }

        /// <summary>
        /// Показывать заблокированных
        /// </summary>
        public bool? ShowBlocked { get; set; }
    }
}
