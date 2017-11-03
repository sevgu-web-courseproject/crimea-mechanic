using Common.Enums;

namespace DataAccessLayer.Models
{
    /// <summary>
    /// Файл автосервиса
    /// </summary>
    public class CarServiceFile : DeletableBaseEntity<long>
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Путь к файлу
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Тип файла
        /// </summary>
        public FileType Type { get; set; }

        /// <summary>
        /// Автосервис
        /// </summary>
        public virtual CarService CarService { get; set; }
    }
}
