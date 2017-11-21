using BusinessLogic.Objects;
using BusinessLogic.Objects.CarService;

namespace BusinessLogic.Managers.Abstraction
{
    public interface ICarServiceManager
    {
        /// <summary>
        /// Редактировать автосервис
        /// </summary>
        void Edit(EditCarServiceDto dto, string currentUserId);

        /// <summary>
        /// Получить информацию об автосервисе
        /// </summary>
        CarServiceInfoDto GetInfo(long carServiceId);

        /// <summary>
        /// Получить список автосервисов
        /// </summary>
        CollectionResult<CarServiceShortInfoDto> GetInfos();

        /// <summary>
        /// Получить список заявок на регистрацию
        /// </summary>
        CollectionResult<CarServiceShortInfoDto> GetRegistrationRequests(CarServiceRegistrationsFilter filter, string currentUserId);

        /// <summary>
        /// Подтвердить регистрацию автосервиса
        /// </summary>
        void ApproveCarService(long carServiceId, string currentUserId);

        /// <summary>
        /// Отклонить регистрацию автосервиса
        /// </summary>
        void RejectCarService(long carServiceId, string currentUserId);

        /// <summary>
        /// Заблокировать автосервис
        /// </summary>
        void BlockCarService(long carServiceId, string currentUserId);
    }
}
