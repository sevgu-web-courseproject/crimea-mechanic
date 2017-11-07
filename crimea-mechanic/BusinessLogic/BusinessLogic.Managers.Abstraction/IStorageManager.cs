using System.Collections.Generic;
using BusinessLogic.Objects;

namespace BusinessLogic.Managers.Abstraction
{
    /// <summary>
    /// Менеджер для работы со статичными данными
    /// </summary>
    public interface IStorageManager
    {
        /// <summary>
        /// Получить все марки автомобилей
        /// </summary>
        /// <param name="searchText">Текст для поиска</param>
        /// <returns></returns>
        List<CarMarkDto> GetCarMarks(string searchText);

        /// <summary>
        /// Получить все модели по идентификатору марки
        /// </summary>
        /// <param name="markId"></param>
        /// <returns></returns>
        List<CarModelDto> GetModels(long markId);

        /// <summary>
        /// Получить все виды работ
        /// </summary>
        /// <returns></returns>
        List<WorkTagDto> GetWorkTags();
    }
}
