using Common.Enums;

namespace BusinessLogic.Objects.Car
{
    /// <summary>
    /// ДТО добавления машины пользователя
    /// </summary>
    public class AddOrEditUserCarDto
    {
        /// <summary>
        /// Идентификатор пользовательской машины
        /// В случае редактирования заполнен
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Наименнование машины (пользовательское)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор модели
        /// В случае добавления заполнен
        /// </summary>
        public long? ModelId { get; set; }

        /// <summary>
        /// Год выпуска автомобиля
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// VIN номер автомобиля
        /// </summary>
        public string Vin { get; set; }

        /// <summary>
        /// Тип топлива
        /// </summary>
        public FuelType FuelType { get; set; }

        /// <summary>
        /// Объем двигателя
        /// </summary>
        public float EngineCapacity { get; set; }
    }
}
