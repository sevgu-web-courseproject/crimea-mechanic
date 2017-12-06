using System.Collections.Generic;
using BusinessLogic.Objects;
using BusinessLogic.Objects.Storage;
using BusinessLogic.Objects.Works;

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

        /// <summary>
        /// Получить список классов работ
        /// </summary>
        /// <returns></returns>
        IEnumerable<WorkClassDto> GetWorkClasses();

        /// <summary>
        /// Добавить новый город
        /// </summary>
        void AddCity(AddCityDto dto, string currentUserId);

        /// <summary>
        /// Добавить новую марку
        /// </summary>
        void AddCarMark(AddCarMarkDto dto, string currentUserId);

        /// <summary>
        /// Добавить новую модель
        /// </summary>
        void AddCarModel(AddCarModelDto dto, string currentUserId);

        /// <summary>
        /// Добавить новый класс работ
        /// </summary>
        void AddWorkClass(AddWorkClassDto dto, string currentUserId);

        /// <summary>
        /// Добавить новый тип работ
        /// </summary>
        void AddWorkType(AddWorkTypeDto dto, string currentUserId);

        /// <summary>
        /// Удалить город
        /// </summary>
        void DeleteCity(long cityId, string currentUserId);

        /// <summary>
        /// Удалить марку
        /// </summary>
        void DeleteMark(long markId, string currentUserId);

        /// <summary>
        /// Удалить модель
        /// </summary>
        void DeleteModel(long modelId, string currentUserId);

        /// <summary>
        /// Удалить класс работ
        /// </summary>
        void DeleteWorkClass(long workClassId, string currentUserId);

        /// <summary>
        /// Удалить тип работы
        /// </summary>
        void DeleteWorkType(long workTypeId, string currentUserId);
    }
}
