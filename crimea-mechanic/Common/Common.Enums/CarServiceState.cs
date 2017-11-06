namespace Common.Enums
{
    public enum CarServiceState : byte
    {
        /// <summary>
        /// Неизвестное состояние
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// На рассмотрении
        /// </summary>
        UnderСonsideration = 10,

        /// <summary>
        /// Активная
        /// </summary>
        Active = 20, 

        /// <summary>
        /// Отклонённая
        /// </summary>
        Rejected = 30,

        /// <summary>
        /// Заблокирована
        /// </summary>
        Blocked = 40
    }
}
