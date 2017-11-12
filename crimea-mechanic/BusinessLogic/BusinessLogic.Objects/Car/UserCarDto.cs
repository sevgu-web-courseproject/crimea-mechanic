namespace BusinessLogic.Objects.Car
{
    /// <summary>
    /// ДТО для отображения машины пользователя
    /// </summary>
    public class UserCarDto
    {
        /// <summary>
        /// Идентификатор машины
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименнование (пользовательское) машины
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Марка машины
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// Модель машины
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// VIN номер машины
        /// </summary>
        public string Vin { get; set; }

        /// <summary>
        /// Год выпуска машины
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// Тип топлива
        /// </summary>
        public string FuelType { get; set; }

        /// <summary>
        /// Объем двигателя
        /// </summary>
        public float EngineCapacity { get; set; }
    }
}
