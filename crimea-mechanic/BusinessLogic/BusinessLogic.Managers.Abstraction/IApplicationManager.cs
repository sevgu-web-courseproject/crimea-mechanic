using System.Collections.Generic;
using BusinessLogic.Objects;
using BusinessLogic.Objects.Application;

namespace BusinessLogic.Managers.Abstraction
{
    /// <summary>
    /// Менеджер для работы с заявками
    /// </summary>
    public interface IApplicationManager
    {
        /// <summary>
        /// Создание новой заявки
        /// </summary>
        void CreateApplication(CreateApplicationDto dto, string currentUserId);

        /// <summary>
        /// Редактирование заявки
        /// Возможно только в состоянии (InSearch)
        /// </summary>
        void EditApplication(EditApplicationDto dto, string currentUserId);

        /// <summary>
        /// Удаление заявки
        /// </summary>
        void DeleteApplication(long applicationId, string currentUserId);

        /// <summary>
        /// Получить информацию о заявке для пользователя
        /// </summary>
        ApplicationInfoForUserDto GetApplicationInfoForUser(long applicationId, string currentUserId);

        /// <summary>
        /// Получить информацию о заявке для автосервиса
        /// </summary>
        ApplicationInfoForServiceDto GetApplicationInfoForService(long applicationId, string currentUserId);

        /// <summary>
        /// Получить информацию о заявке для администратора
        /// </summary>
        ApplicationInfoForAdministratorDto GetApplicationInfoForAdministrator(long applicationId, string currentUserId);

        /// <summary>
        /// Получить информацию о заявках пользователя
        /// </summary>
        CollectionResult<ApplicationShortInfoForUserDto> GetApplicationsInfoForUser(ApplicationsFilter filter, string currentUserId);

        /// <summary>
        /// Получить информацию о заявках автосервиса
        /// </summary>
        CollectionResult<ApplicationShortInfoForServiceDto> GetApplicationsForService(ApplicationsFilter filter, string currentUserId);

        /// <summary>
        /// Получить информацию о заявках для администратора
        /// </summary>
        CollectionResult<ApplicationShortInfoForAdministratorDto> GetApplicationsForAdministrator(ApplicationsFilter filter, string currentUserId);

        /// <summary>
        /// Получить заявки из пула необработанных заявок для автосервиса
        /// </summary>
        CollectionResult<ApplicationShortInfoForServiceDto> GetApplicationsFromPool(ApplicationsPoolFilter filter, string currentUserId);

        /// <summary>
        /// Добавить предложение для заявки
        /// </summary>
        void AddOffer(AddOfferDto dto, string currentUserId);

        /// <summary>
        /// Удалить предложение для заявки
        /// </summary>
        void DeleteOffer(long offerId, string currentUserId);

        /// <summary>
        /// Принять предложение
        /// </summary>
        void AcceptOffer(long offerId, string currentUserId);

        /// <summary>
        /// Отклонить исполнение заявки
        /// </summary>
        void RejectApplication(long applicationId, string currentUserId);

        /// <summary>
        /// Завершить выполнение заявки
        /// </summary>
        void CompleteApplication(long applicationId, string currentUserId);

        /// <summary>
        /// Получить список марок автомобилей, которые находятся в пуле
        /// </summary>
        /// <returns></returns>
        IEnumerable<CarMarkDto> GetAvailableMarksFromPool(string currentUserId);
    }
}
