﻿using BusinessLogic.Objects;
using BusinessLogic.Objects.CarService;

namespace BusinessLogic.Managers.Abstraction
{
    public interface ICarServiceManager
    {
        /// <summary>
        /// Редактировать автосервис
        /// </summary>
        void Edit(EditCarServiceDto dto, string directory, string currentUserId);

        /// <summary>
        /// Получить информацию для редактирования автосервиса
        /// </summary>
        CarServiceInfoForEditDto GetInfoForEdit(string currentUserId);

        /// <summary>
        /// Получить профиль автосервиса
        /// </summary>
        CarServiceInfoDto GetProfile(string currentUserId);

        /// <summary>
        /// Получить информацию об автосервисе
        /// </summary>
        CarServiceInfoDto GetInfo(long carServiceId, string currentUserId);

        /// <summary>
        /// Получить информацию об регистрации автосервиса
        /// </summary>
        RegistrationRequestInfoDto GetRegistrationRequest(long carServiceId, string currentUserId);

        /// <summary>
        /// Получить список автосервисов
        /// </summary>
        CollectionResult<CarServiceShortInfoDto> GetInfos(CarServiceFilter filter);

        /// <summary>
        /// Получить список заявок на регистрацию
        /// </summary>
        CollectionResult<RegistrationRequestShortInfoDto> GetRegistrationRequests(CarServiceRegistrationsFilter filter, string currentUserId);

        /// <summary>
        /// Подтвердить регистрацию автосервиса
        /// </summary>
        void ApproveCarService(long carServiceId, string currentUserId);

        /// <summary>
        /// Отклонить регистрацию автосервиса
        /// </summary>
        void RejectCarService(RejectDto dto, string currentUserId);

        /// <summary>
        /// Заблокировать автосервис
        /// </summary>
        void BlockCarService(long carServiceId, string currentUserId);

        /// <summary>
        /// Разблокировать автосервис
        /// </summary>
        void UnBlockCarService(long carServiceId, string currentUserId);
    }
}
