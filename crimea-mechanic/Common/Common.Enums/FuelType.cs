namespace Common.Enums
{
    /// <summary>
    /// Вид топлива
    /// </summary>
    public enum FuelType : byte
    {
        /// <summary>
        /// Неизвестный
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Бензин
        /// </summary>
        Benzine = 10,

        /// <summary>
        /// Дизель
        /// </summary>
        Diesel = 20,

        /// <summary>
        /// Другой
        /// </summary>
        Another = 30
    }
}
