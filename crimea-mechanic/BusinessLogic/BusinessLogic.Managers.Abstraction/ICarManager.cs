using System.Collections.Generic;
using BusinessLogic.Objects.Car;

namespace BusinessLogic.Managers.Abstraction
{
    public interface ICarManager
    {
        /// <summary>
        /// Добавить новую машину пользователя
        /// </summary>
        void AddCar(AddOrEditUserCarDto dto, string currentUserId);

        /// <summary>
        /// Отредактировать машину пользователя
        /// </summary>
        void EditCar(AddOrEditUserCarDto dto, string currentUserId);

        /// <summary>
        /// Удалить машину пользователя
        /// </summary>
        void DeleteCar(long userCarId, string currentUserId);

        /// <summary>
        /// Восстановить машину пользователя
        /// </summary>
        void RestoreCar(long userCarId, string currentUserId);

        /// <summary>
        /// Получить информацию о машине пользователя
        /// </summary>
        /// <returns></returns>
        UserCarDto GetCar(long userCarId, string currentUserId);

        /// <summary>
        /// Получить информацию о машинах пользователя
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserCarDto> GetCars(FilterUserCar filter, string currentUserId);
    }
}
