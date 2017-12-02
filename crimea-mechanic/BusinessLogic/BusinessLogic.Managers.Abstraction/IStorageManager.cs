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
        IEnumerable<CarMarkDto> GetCarMarks(string searchText);

        /// <summary>
        /// Получить все модели по идентификатору марки
        /// </summary>
        /// <param name="markId"></param>
        /// <returns></returns>
        IEnumerable<CarModelDto> GetModels(long markId);

        /// <summary>
        /// Получить список городов
        /// </summary>
        /// <returns></returns>
        IEnumerable<CityDto> GetCities();
    }
}
