namespace BusinessLogic.Objects.Storage
{
    /// <summary>
    /// ДТО добавления типа работы
    /// </summary>
    public class AddWorkTypeDto
    {
        /// <summary>
        /// Идентификатор класса работ
        /// </summary>
        public long WorkClassId { get; set; }

        /// <summary>
        /// Наименнование типа работ
        /// </summary>
        public string Name { get; set; }
    }
}
