namespace Common.Enums
{
    /// <summary>
    /// Состояния заявки
    /// </summary>
    public enum ApplicationState : byte
    {
        /// <summary>
        /// Неизвестное состояние
        /// </summary>
        Unknown = 0,
        
        /// <summary>
        /// Удалена
        /// </summary>
        Deleted = 5,

        /// <summary>
        /// В поиске исполнителя
        /// </summary>
        InSearch = 10,

        /// <summary>
        /// На исполнении
        /// </summary>
        InProcessing = 15,

        /// <summary>
        /// Отменена заказчиком/исполнеителем
        /// </summary>
        Rejected = 20,

        /// <summary>
        /// Исполнена
        /// </summary>
        Completed = 25
    }
}
