using BusinessLogic.Objects.Application;

namespace BusinessLogic.Objects.Car
{
    public class FilterUserCar : BaseFilter
    {
        /// <summary>
        /// Признак показывать удаленные автомобили
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
