namespace BusinessLogic.Objects.CarService
{
    /// <summary>
    /// ДТО для отказа регистрации автосервиса
    /// </summary>
    public class RejectDto
    {
        /// <summary>
        /// Идентификатор автосервиса
        /// </summary>
        public long CarServiceId { get; set; }

        /// <summary>
        /// Причина отказа
        /// </summary>
        public string Reason { get; set; }
    }
}
